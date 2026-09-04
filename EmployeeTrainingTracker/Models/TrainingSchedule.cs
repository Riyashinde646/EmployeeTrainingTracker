using System;

namespace EmployeeTrainingTracker.Models
{
    public class TrainingSchedule
    {
        public int ScheduleId { get; set; }

        public int TrainerId { get; set; }

        public int TopicId { get; set; }

        public DateTime TrainingDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}