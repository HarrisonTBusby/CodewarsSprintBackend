namespace CodewarsSprintBackend.Models
{
    public class UserModel
    {
         public int Id { get; set; }
         public bool isAdmin { get; set; }
        public string? Username { get; set; }
        public string? Salt { get; set; }
        public string? Hash { get; set; }

        public UserModel(){}
    }
}