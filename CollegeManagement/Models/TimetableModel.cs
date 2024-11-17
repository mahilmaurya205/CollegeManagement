namespace CollegeManagement.Models
{
    public class TimetableModel
    {
        public int ClassID { get; set; }
        public int SubjectID { get; set; }
        public string Day { get; set; }
        public string TimeSlot { get; set; }
        public string Room { get; set; }
    }
}
