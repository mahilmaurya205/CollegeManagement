using System.ComponentModel.DataAnnotations;

namespace CollegeManagement.Entities
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
    }
}
