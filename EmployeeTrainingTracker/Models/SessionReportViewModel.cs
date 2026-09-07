using System;

namespace EmployeeTrainingTracker.Models
{
    public class SessionReportViewModel
    {
        public int SessionId { get; set; }

        public int ScheduleId { get; set; }

        public string TopicName { get; set; }

        public string SubTopicName { get; set; }

        public bool SessionDone { get; set; }

        public string SessionFeedback { get; set; }

        public string Resources { get; set; }
    }
}