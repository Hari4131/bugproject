using Ticket_ManagementAPI.Dto;

namespace Ticket_ManagementAPI.IRepository
{
    public interface IHome
    {
        Task<object> Register(RegisterDto model);
        Task<object> Login(LoginDto model);
        Task<object> EntityType();
        Task<object> TicketLogin(LoginTicket loginTicket);
    }
}
