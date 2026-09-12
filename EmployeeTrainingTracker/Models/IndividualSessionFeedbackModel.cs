using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeTrainingTracker.Models
{
    public class IndividualSessionFeedbackModel
    {
        public int FeedbackId { get; set; }

        public int SessionId { get; set; }

        public int TraineeId { get; set; }

        public int ScheduleId { get; set; }

        public string TrainerName { get; set; }

        public string TraineeName { get; set; }

        public string TopicName { get; set; }

        public string SubTopics { get; set; }

        public DateTime TrainingDate { get; set; }

        public bool SessionCompleted { get; set; }

        public string Feedback { get; set; }

        

        public bool HasFeedback { get; set; }


    }
}