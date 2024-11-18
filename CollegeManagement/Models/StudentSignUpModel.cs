namespace CollegeManagement.Models
{
    public class StudentSignUpModel
    {
        public int SID { get; set; }  // Primary Key
        public string Name { get; set; }
        public string Email { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Password { get; set; }
    }
}
