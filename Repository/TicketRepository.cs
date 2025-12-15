using Microsoft.EntityFrameworkCore;
using Ticket_ManagementAPI.Data;
using Ticket_ManagementAPI.Dto;
using Ticket_ManagementAPI.IRepository;
using Ticket_ManagementAPI.Model;

namespace Ticket_ManagementAPI.Repository
{
    public class TicketRepository : ITicket
    {
        private readonly ApplicationDbContext _context;


        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;


        }



        public async Task<object> UpdateTicket(UpdateTicketDto updateTicketDto)
        {
            try
            {

                var existingTicket = await _context.Tickets
                    .FirstOrDefaultAsync(t => t.Tic_UniqueNum == updateTicketDto.Tic_UniqueNum);


                if (existingTicket == null)
                {
                    return new
                    {
                        success = false,
                        message = "The bug_test number was not found."
                    };
                }

                existingTicket.Tic_Name = updateTicketDto.Tic_Name;
                existingTicket.Subject = updateTicketDto.Subject;
                existingTicket.Description = updateTicketDto.Description;
                existingTicket.Tic_Status = updateTicketDto.Tic_Status;
                existingTicket.Description = updateTicketDto.Description;

                await _context.SaveChangesAsync();

                return new
                {
                    success = true,
                    message = "Ticket updated successfully!"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "An error occurred while updating the bug_test.",
                    error = ex.Message
                };
            }
        }



        public async Task<object> RegisterTicket(RegisterTicketDto registerTicketDto)
        {
            try
            {
                var bug_test = new Tickets
                {
                    Tic_Name = registerTicketDto.Tic_Name,     
                    Subject = registerTicketDto.Subject,
                    Description = registerTicketDto.Description,
                    Tic_Status = 1, 
                    Tic_UniqueNum = new Random().Next(100000, 999999) ,
                    Tic_ClientId = registerTicketDto.Tic_ClientId
                };

                _context.Tickets.Add(bug_test);
                await _context.SaveChangesAsync();

                return new
                {
                    success = true,
                    message = "Ticket registered successfully!",
                    bug_testId = bug_test.Tic_Id,
                    bug_testNumber = bug_test.Tic_UniqueNum
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "An error occurred while processing the bug_test registration.",
                    error = ex.Message
                };
            }
        }

        public async Task<object> GetAllTicketsForClient(int tic_ClientId)
        {
            try
            {
                var bug_tests = await (from t in _context.Tickets
                                     join s in _context.Ticket_Statuses
                                     on t.Tic_Status equals s.Tic_Sta_Id
                                     where t.Tic_ClientId == tic_ClientId
                                     orderby t.Tic_Id descending
                                     select new GetAllTickets
                                     {
                                         Tic_UniqueNum = t.Tic_UniqueNum,
                                         Subject = t.Subject,
                                         Description = t.Description,
                                         Tic_StatusDes = s.Tic_Sta_Name
                                     }).ToListAsync();

                return new
                {
                    success = true,
                    message = "Tickets fetched successfully!",
                    data = bug_tests
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "An error occurred while fetching bug_tests.",
                    error = ex.Message
                };
            }
        }


        public async Task<object> GetTicketsByStatusId(int tic_StaId, int tic_ClientId)
        {
            try
            {
                var bug_tests = await (from t in _context.Tickets
                                     join s in _context.Ticket_Statuses
                                     on t.Tic_Status equals s.Tic_Sta_Id
                                     where t.Tic_Status == tic_StaId && t.Tic_ClientId ==tic_ClientId
                                     orderby t.Tic_Id descending
                                     select new GetAllTickets
                                     {
                                         Tic_UniqueNum = t.Tic_UniqueNum,
                                         Subject = t.Subject,
                                         Description = t.Description,
                                         Tic_StatusDes = s.Tic_Sta_Name
                                     }).ToListAsync();

                return new
                {
                    success = true,
                    message = "Tickets fetched successfully!",
                    data = bug_tests
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "An error occurred while fetching bug_tests.",
                    error = ex.Message
                };
            }
        }


        public async Task<object> GetTicketsByUniqueId(int tic_UniqueNum, int tic_ClientId)
        {
            try
            {
                var bug_tests = await (from t in _context.Tickets
                                     join s in _context.Ticket_Statuses
                                     on t.Tic_Status equals s.Tic_Sta_Id
                                     where t.Tic_UniqueNum == tic_UniqueNum && t.Tic_ClientId == tic_ClientId
                                     orderby t.Tic_Id descending
                                     select new GetAllTickets
                                     {
                                         Tic_UniqueNum = t.Tic_UniqueNum,
                                         Subject = t.Subject,
                                         Description = t.Description,
                                         Tic_StatusDes = s.Tic_Sta_Name
                                     }).ToListAsync();

                return new
                {
                    success = true,
                    message = "Tickets fetched successfully!",
                    data = bug_tests
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "An error occurred while fetching bug_tests.",
                    error = ex.Message
                };
            }
        }


        public async Task<object> GetAllTicketStatusDD()
        {
            try
            {
                var bug_tests = await (from t in _context.Ticket_Statuses
                                     orderby t.Tic_Sta_Id descending
                                     select new Ticket_Status
                                     {
                                         Tic_Sta_Id = t.Tic_Sta_Id,
                                         Tic_Sta_Name = t.Tic_Sta_Name
                                     }).ToListAsync();

                return new
                {
                    success = true,
                    message = "Tickets fetched successfully!",
                    data = bug_tests
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "An error occurred while fetching bug_tests.",
                    error = ex.Message
                };
            }

        }


        public async Task<object> GetAllTickets()
        {
            try
            {
                var bug_tests = await (from f in _context.Client_Users
                                     join t in _context.Tickets on f.Cli_Id equals t.Tic_ClientId
                                     join s in _context.Ticket_Statuses
                                     on t.Tic_Status equals s.Tic_Sta_Id
                                     orderby t.Tic_Id descending
                                     select new GetAllTickets
                                     {
                                          Cli_EmailId= f.Cli_EmailId,
                                     Cli_MobileNumber= f.Cli_MobileNumber,
                                    Cli_Name= f.Cli_Name,
                                         Cli_Description = f.Cli_Description,
                                         Cli_Address = f.Cli_Address,
                                         Cli_Id = f.Cli_Id,
                                         Tic_Name = t.Tic_Name,
                                      Tic_UniqueNum = t.Tic_UniqueNum,
                                         Subject = t.Subject,
                                         Description = t.Description,
                                         Tic_StatusDes = s.Tic_Sta_Name,
                                         Tic_StatusId = s.Tic_Sta_Id
                                     }).ToListAsync();

                return new
                {
                    success = true,
                    message = "Tickets fetched successfully!",
                    data = bug_tests
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "An error occurred while fetching bug_tests.",
                    error = ex.Message
                };
            }
        }
    }
}



//{
//    "email": "ashru1099@gmail.com",
//  "password": "Ashru562@"
//}




