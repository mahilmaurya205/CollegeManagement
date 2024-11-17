using System.ComponentModel.DataAnnotations;

namespace CollegeManagement.Entities
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string RollNumber { get; set; }
    }
}
