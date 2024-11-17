using CollegeManagement.Entities;

namespace CollegeManagement.Models
{
    public class LoginModel
    {
        public int LoginID { get; set; }   // Primary Key
        public string Email { get; set; }   // User's email
        public string Password { get; set; }   // The hashed password
        public bool IsActive { get; set; }  // Account status (active or inactive)
        public int StudentID { get; set; }  // Foreign key to Student

        public virtual StudentSignUp StudentSignUp { get; set; }
    }
}
