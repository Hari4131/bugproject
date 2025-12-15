using Ticket_ManagementAPI.Dto;
using Ticket_ManagementAPI.Model;

namespace Ticket_ManagementAPI.IRepository
{
    public interface ITicket
    {
        Task<object> RegisterTicket(RegisterTicketDto registerTicketDto);
        Task<object> UpdateTicket(UpdateTicketDto updateTicketDto);
        Task<object> GetAllTickets();
        Task<object> GetTicketsByStatusId(int tic_StaId, int tic_ClientId);
        Task<object> GetTicketsByUniqueId(int tic_UniqueNum, int tic_ClientId);
        Task<object> GetAllTicketStatusDD();
        Task<object> GetAllTicketsForClient(int tic_ClientId);


    }
}
