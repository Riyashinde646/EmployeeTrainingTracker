using System;
using System.Data;
using System.Data.SqlClient;
using EmployeeTrainingTracker.Models;

namespace EmployeeTrainingTracker.DAL
{
    public class TrainerDashboardDAL
    {
        private string connectionString =
            System.Configuration.ConfigurationManager
            .ConnectionStrings["EmployeeTrainingTrackerDB"]
            .ConnectionString;

        public TrainerDashboardModel GetDashboardCounts(int trainerId)
        {
            TrainerDashboardModel dashboard =
                new TrainerDashboardModel();

            using (SqlConnection con =
                new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_GetTrainerDashboardCounts", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@TrainerId", trainerId);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    dashboard.MySessions =
                        Convert.ToInt32(dr["MySessions"]);

                    dashboard.CompletedSessions =
                        Convert.ToInt32(dr["CompletedSessions"]);

                    dashboard.PendingSessions =
                        Convert.ToInt32(dr["PendingSessions"]);
                }
            }

            return dashboard;
        }
    }
}