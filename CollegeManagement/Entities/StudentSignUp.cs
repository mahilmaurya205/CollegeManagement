using System.ComponentModel.DataAnnotations;

namespace CollegeManagement.Entities
{
    public class StudentSignUp
    {
        public int SID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string Password { get; set; }
    }
}
