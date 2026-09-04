using System;

namespace EmployeeTrainingTracker.Models
{
    public class Training
    {
        public int TrainingId { get; set; }

        public string TrainingName { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}