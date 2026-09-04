using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace EmployeeTrainingTracker.Models
{
    public class TrainerModel
    {
        public int TrainerID { get; set; }

        public int UserID { get; set; }

        public string TrainerName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        // while adding trainer 
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}