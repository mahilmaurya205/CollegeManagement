using System.ComponentModel.DataAnnotations;

namespace CollegeManagement.Entities
{
    public class Login
    {
        public int LoginID { get; set; }   
        public string Email { get; set; }  
        public string Password { get; set; }   
        public bool IsActive { get; set; } 
        public int StudentID { get; set; }  

        public virtual StudentSignUp StudentSignUp { get; set; }
    }
}
