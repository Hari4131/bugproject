namespace Ticket_ManagementAPI.Dto
{
    public class RegisterTicketDto
    {
        public string Tic_Name { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int Tic_ClientId { get; set; }
    }

    public class UpdateTicketDto
    {

        public int Tic_UniqueNum { get; set; }
        public int Tic_Status { get; set; }
        public string Tic_Name { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

    }

}

