using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeTrainingTracker.Models
{
    public class ManagerDashboardModel
    {
        public int TotalTrainers { get; set; }

        public int TotalTrainees { get; set; }

        public int TotalAssessmentsDone { get; set; }

        public int CompletedSessions { get; set; }
    }
}