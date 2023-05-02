namespace CodewarsSprintBackend.Models.DTO
{
    public class LoginDTO
    {
        public bool isAdmin { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}