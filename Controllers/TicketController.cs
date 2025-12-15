using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket_ManagementAPI.Dto;
using Ticket_ManagementAPI.IRepository;
using Ticket_ManagementAPI.Repository;

namespace Ticket_ManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicket _bug_testsRepository;

        public TicketController(ITicket bug_testsRepository)
        {
            _bug_testsRepository = bug_testsRepository;
        }


        [HttpPost("RegisterTicket")]
        public async Task<IActionResult> RegisterTicket([FromBody] RegisterTicketDto registerTicketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bug_testsRepository.RegisterTicket(registerTicketDto);
            return Ok(result);
        }

   
        [HttpPut("UpdateTicket")]
        public async Task<IActionResult> UpdateTicket([FromBody] UpdateTicketDto updateTicketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bug_testsRepository.UpdateTicket(updateTicketDto);
            return Ok(result);
        }

      
        [HttpGet("GetAllTickets")]
        public async Task<IActionResult> GetAllTickets()
        {
            var result = await _bug_testsRepository.GetAllTickets();
            return Ok(result);
        }


        [HttpGet("GetTicketsByStatusId")]
        public async Task<IActionResult> GetTicketsByStatusId(TicketsDto bug_testsDto)
        {
            var result = await _bug_testsRepository.GetTicketsByStatusId(bug_testsDto.tic_StaId, bug_testsDto.tic_ClientId);
            return Ok(result);

        }

        [HttpGet("GetTicketsByUniqueId")]
        public async Task<IActionResult> GetTicketsByUniqueId(TicketUniqueId bug_testUniqueId)
        {
            var result = await _bug_testsRepository.GetTicketsByUniqueId(bug_testUniqueId.tic_UniqueNum, bug_testUniqueId.tic_ClientId);
            return Ok(result);

        }



        [HttpGet("GetAllTicketStatusDD")]
        public async Task<IActionResult> GetAllTicketStatusDD()
        {
            var result = await _bug_testsRepository.GetAllTicketStatusDD();
            return Ok(result);

        }



        [HttpGet("GetAllTicketsForClient")]
        public async Task<IActionResult> GetAllTicketsForClient(int tic_ClientId)
        {
            var result = await _bug_testsRepository.GetAllTicketsForClient(tic_ClientId);
            return Ok(result);

        }


        


    }
}
