namespace WebApplication1.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public string? RefreshToken { get; set; } = null;
    }
}
