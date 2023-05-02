namespace CodewarsSprintBackend.Models.DTO
{
    public class CreateAccountDTO
    {
        public int Id { get; set; }
        public bool isAdmin { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}