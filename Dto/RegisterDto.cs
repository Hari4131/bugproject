namespace Ticket_ManagementAPI.Dto
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string MobileNumber { get; set; }

        public string Address { get; set; }
        public string Description { get; set; }

        public int EntityType { get; set; }

    }


    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginTicket
    {

        public string Email { get; set; }
        public int Tic_UniqueNo { get; set; }
    }

    public class LoginDetails
    {
        public bool success { get; set; }
        public string token { get; set; }
        public object expiration { get; set; }
        public string role { get; set; }
        public string Cli_MobileNumber { get; set; }
        public string Cli_EmailId { get; set; }
        public string Cli_Name { get; set; }
        public int EntityType { get; set; }
        public string Cli_Address { get; set; }
        public int Cli_Id { get; set; }






    } 
}
