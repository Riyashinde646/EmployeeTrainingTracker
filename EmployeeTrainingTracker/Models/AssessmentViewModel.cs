using System;

namespace EmployeeTrainingTracker.Models
{
    public class AssessmentViewModel
    {
        public int AssessmentId { get; set; }

        public int ScheduleId { get; set; }

        public int TraineeId { get; set; }

        public int SubTopicId { get; set; }

        public string TraineeName { get; set; }

        public string TopicName { get; set; }

        public string SubTopicName { get; set; }

        public bool AssignmentDone { get; set; }

        public bool TestConducted { get; set; }

        public int? TestMarks { get; set; }

        public string IndividualFeedback { get; set; }
    }
}