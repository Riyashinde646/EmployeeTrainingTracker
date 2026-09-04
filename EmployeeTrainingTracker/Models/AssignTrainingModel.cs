using System;
using System.Collections.Generic;

namespace EmployeeTrainingTracker.Models
{
    public class AssignTrainingModel
    {
        public int ScheduleId { get; set; }

        public int TrainerId { get; set; }

        public int TopicId { get; set; }

        public List<int> SubTopicIds { get; set; }

        public List<int> TraineeIds { get; set; }

        public DateTime TrainingDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}