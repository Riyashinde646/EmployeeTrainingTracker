using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeTrainingTracker.Models
{
    public class TrainerDashboardModel
    {
        public int MySessions { get; set; }

        public int CompletedSessions { get; set; }

        public int PendingSessions { get; set; }
    }
}