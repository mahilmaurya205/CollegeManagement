﻿using System.ComponentModel.DataAnnotations;

namespace CollegeManagement.Entities
{
    public class TeacherSignUp
    {
        public int TeacherID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string AccessKey { get; set; }
        public string Password { get; set; }
    }
}
