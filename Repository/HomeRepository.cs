using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticket_ManagementAPI.Data;
using Ticket_ManagementAPI.Dto;
using Ticket_ManagementAPI.IRepository;
using Ticket_ManagementAPI.Model;

namespace Ticket_ManagementAPI.Repository
{
    public class HomeRepository : IHome
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public HomeRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _config = config;
        }

        
        public async Task<object> Register(RegisterDto model)
        {
            try
            {
                
                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                    return new { success = false, message = "User already exists!" };

                
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.MobileNumber,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                };

               
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return new { success = false, message = "User creation failed!", errors = result.Errors };

               
                if (!await _roleManager.RoleExistsAsync("Client"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Client"));
                }

               
                await _userManager.AddToRoleAsync(user, "Client");

                
                var client = new Client_User
                {
                    Cli_Name = model.FullName,
                    Cli_EmailId = model.Email,
                    Cli_MobileNumber = model.MobileNumber,
                    Cli_Address = model.Address,
                    Cli_Description = model.Description,
                    EntityType = model.EntityType
                };

                _context.Client_Users.Add(client);
                await _context.SaveChangesAsync();

                return new { success = true, message = "Client registered successfully!" };
            }
            catch (Exception ex)
            {
              
                return new
                {
                    success = false,
                    message = "An error occurred while registration request.",
                    error = ex.Message 
                };
            }
        }
        public async Task<object> Login(LoginDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return new { success = false, message = "Invalid email or password" };

                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (!result.Succeeded)
                    return new { success = false, message = "Invalid email or password" };

                var clientResult = await _context.Client_Users.FirstOrDefaultAsync(c => c.Cli_MobileNumber == user.PhoneNumber);
                
                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                    authClaims.Add(new Claim(ClaimTypes.Role, role));

                var authSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["Jwt:Key"])
                );

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                if(roles.FirstOrDefault() == "Admin")
                {
                    return new
                    {
                        success = true,
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        role = roles.FirstOrDefault(),

                    };

                }

                return new LoginDetails
                {
                    success = true,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    role = roles.FirstOrDefault(),
                    Cli_Id = clientResult.Cli_Id,
                    Cli_MobileNumber = clientResult.Cli_MobileNumber,
                    Cli_EmailId = clientResult.Cli_EmailId,
                    Cli_Name = clientResult.Cli_Name,
                    EntityType = clientResult.EntityType,
                    Cli_Address = clientResult.Cli_Address,
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "An error occurred while processing your login request.",
                    error = ex.Message 
                };
            }
        }

        public async Task<object> EntityType()
        {
            try
            {
                var result = await _context.Entities
                    .OrderBy(e => e.Ent_Id)
                    .Select(e => new
                    {
                        Id = e.Ent_Id,
                        Name = e.Ent_Name
                    })
                    .ToListAsync();

                return new { success = true, data = result };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Error fetching entity types",
                    error = ex.Message
                };
            }
        }

        public async Task<object> TicketLogin(LoginTicket loginTicket)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginTicket.Email);
                if (user == null)
                    return new
                    { success = false, message = "EmailID Not found" };
                        var roles = await _userManager.GetRolesAsync(user);
                var client = await _context.Client_Users
                    .FirstOrDefaultAsync(c => c.Cli_EmailId == loginTicket.Email);

                if (client == null)
                {
                    return new { success = false, message = "Invalid EmailID" };
                }

               
                var bug_testCheck = await _context.Tickets
                    .FirstOrDefaultAsync(t => t.Tic_UniqueNum == loginTicket.Tic_UniqueNo
                                           && t.Tic_ClientId == client.Cli_Id);

                if (bug_testCheck == null)
                {
                    return new { success = false, message = "Invalid Ticket Number" };
                }

               
                var result = await (
                    from d in _context.Client_Users
                    join e in _context.Entities
                        on d.EntityType equals e.Ent_Id

                   
                    join t in _context.Tickets
                        on d.Cli_Id equals t.Tic_ClientId into bug_testGroup
                    from t in bug_testGroup.DefaultIfEmpty()

                        
                    join s in _context.Ticket_Statuses
                        on t.Tic_Status equals s.Tic_Sta_Id into statusGroup
                    from s in statusGroup.DefaultIfEmpty()

                    where d.Cli_Id == client.Cli_Id
                    orderby t.Tic_Id descending

                    select new TicketLoginDetails
                    {
                        Role = roles.FirstOrDefault(),
                        Cli_EmailId = d.Cli_EmailId,
                        Cli_MobileNumber = d.Cli_MobileNumber,
                        Cli_Name = d.Cli_Name,
                        Cli_EntityDetails = e.Ent_Name,

                        getAllTickets = t == null ? null : new GetAllTickets
                        {
                            Tic_UniqueNum = t.Tic_UniqueNum,
                            Subject = t.Subject,
                            Description = t.Description,
                            Tic_StatusDes = s != null ? s.Tic_Sta_Name : null
                        }
                    }
                ).ToListAsync();

                return new { success = true, data = result };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Error fetching bug_test details",
                    error = ex.Message
                };
            }
        }






    }
}
