using System;

namespace EmployeeTrainingTracker.Models
{
    public class TraineeModel
    {
        public int TraineeID { get; set; }

        public int UserID { get; set; }

        public string TraineeName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }
    }
}