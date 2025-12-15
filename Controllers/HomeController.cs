using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticket_ManagementAPI.Dto;
using Ticket_ManagementAPI.IRepository;

namespace Ticket_ManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IHome _homeRepo;

        public HomeController(IHome authRepo)
        {
            _homeRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _homeRepo.Register(model);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _homeRepo.Login(model);
            return Ok(result);
        }

        [HttpGet("EntityType")]
        public async Task<IActionResult> EntityType()
        {
            var result = await _homeRepo.EntityType();
            return Ok(result);

        }

        [HttpPost("TicketLogin")]
        public async Task<IActionResult> TicketLogin(LoginTicket loginTicket)
        {
            var result = await _homeRepo.TicketLogin(loginTicket);
            return Ok(result);

        }




    }
}
