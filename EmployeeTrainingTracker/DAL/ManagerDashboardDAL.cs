using System;
using System.Data;
using System.Data.SqlClient;
using EmployeeTrainingTracker.Models;

namespace EmployeeTrainingTracker.DAL
{
    public class ManagerDashboardDAL
    {
        private string connectionString =
            System.Configuration.ConfigurationManager
            .ConnectionStrings["EmployeeTrainingTrackerDB"]
            .ConnectionString;

        public ManagerDashboardModel GetDashboardCounts()
        {
            ManagerDashboardModel dashboard =
                new ManagerDashboardModel();

            using (SqlConnection con =
                new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_GetManagerDashboardCounts", con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    dashboard.TotalTrainers =
                        Convert.ToInt32(dr["TotalTrainers"]);

                    dashboard.TotalTrainees =
                        Convert.ToInt32(dr["TotalTrainees"]);

                    dashboard.TotalAssessmentsDone =
                        Convert.ToInt32(dr["TotalAssessmentsDone"]);

                    dashboard.CompletedSessions =
                        Convert.ToInt32(dr["CompletedSessions"]);
                }
            }

            return dashboard;
        }
    }
}