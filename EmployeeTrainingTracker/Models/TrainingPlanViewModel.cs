using System;

// used this as overall plan needs to display these 

namespace EmployeeTrainingTracker.Models
{
    public class TrainingPlanViewModel
    {
        public int ScheduleId { get; set; }

        public string TrainerName { get; set; }

        public string TopicName { get; set; }

        public string SubTopicName { get; set; }

        public DateTime TrainingDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}