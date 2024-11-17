﻿using System.ComponentModel.DataAnnotations;

namespace CollegeManagement.Entities
{
    public class AccessKey
    {
        public int KeyID { get; set; }
        public string Key { get; set; }
        public bool IsUsed { get; set; }

    }
}
