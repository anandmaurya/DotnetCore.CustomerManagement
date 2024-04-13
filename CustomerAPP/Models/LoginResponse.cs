namespace CustomerAPP.Models
{
    public class LoginResponse
    {
        public bool Result { get; set; }
        public string APIToken { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime Expiration { get; set; }
    }
}
