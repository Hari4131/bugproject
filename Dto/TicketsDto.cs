namespace Ticket_ManagementAPI.Dto
{
    public class TicketsDto
    {
        public int tic_StaId { get; set; }
        public int tic_ClientId { get; set; }

    }

    public class TicketUniqueId {
        public int tic_UniqueNum { get; set; }
        public int tic_ClientId { get; set; }

    }
}
