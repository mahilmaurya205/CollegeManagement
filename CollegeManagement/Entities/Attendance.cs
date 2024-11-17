using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace CollegeManagement.Entities
{
    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
