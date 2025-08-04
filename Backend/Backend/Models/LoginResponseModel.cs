namespace Backend.Models
{
    public class LoginResponseModel
    {
        public required string username { get; set; }

        public int user_id { get; set; }

        public required string token { get; set; }

        public required int expiration { get; set; }
        public required string fullName { get; set; }
    }
}
