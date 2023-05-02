namespace CodewarsSprintBackend.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public int KataId { get; set; }
        public string? KataName { get; set; }
        public string? KataLang { get; set; }
        public int UserId { get; set; }
        public string? Username {get; set; }
        public string? AssignedBy { get; set; }
        public bool isLocked { get; set; }
        public bool isCompleted { get; set; }
        public bool isDeleted { get; set; }
        public ReservationModel(){}
    }
}