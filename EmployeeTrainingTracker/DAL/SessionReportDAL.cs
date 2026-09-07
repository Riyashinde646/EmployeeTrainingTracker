using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EmployeeTrainingTracker.Models;

namespace EmployeeTrainingTracker.DAL
{
    public class SessionReportDAL
    {
        string connectionString =
            ConfigurationManager.ConnectionStrings["EmployeeTrainingTrackerDB"].ConnectionString;

        public List<SessionReportViewModel> GetSessionReports()  //Get the session report data from SQL Server and give it to the Controller.
        {
            List<SessionReportViewModel> reports =
                new List<SessionReportViewModel>();

            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand("sp_GetSessionReports", con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    SessionReportViewModel report =
                        new SessionReportViewModel();

                    report.SessionId =
                        Convert.ToInt32(dr["SessionId"]);

                    report.ScheduleId =
                        Convert.ToInt32(dr["ScheduleId"]);

                    report.TopicName =
                        dr["TopicName"].ToString();

                    report.SubTopicName =
                        dr["SubTopicName"].ToString();

                    report.SessionDone =
                        Convert.ToBoolean(dr["SessionDone"]);

                    report.SessionFeedback =
                        dr["SessionFeedback"].ToString();

                    report.Resources =
                        dr["Resources"].ToString();

                    reports.Add(report);
                }
            }

            return reports;
        }

        public List<SessionReportViewModel> GetTrainerSessionReports(int trainerId) // for trainers
        {
            List<SessionReportViewModel> reports =
                new List<SessionReportViewModel>();

            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand("sp_GetTrainerSessionReports", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TrainerId", trainerId);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    SessionReportViewModel report =
                        new SessionReportViewModel();

                    report.SessionId =
                        Convert.ToInt32(dr["SessionId"]);

                    report.ScheduleId =
                        Convert.ToInt32(dr["ScheduleId"]);

                    report.TopicName =
                        dr["TopicName"].ToString();

                    report.SubTopicName =
                        dr["SubTopicName"].ToString();

                    report.SessionDone =
                        Convert.ToBoolean(dr["SessionDone"]);

                    report.SessionFeedback =
                        dr["SessionFeedback"].ToString();

                    report.Resources =
                        dr["Resources"].ToString();

                    reports.Add(report);
                }
            }

            return reports;
        }

        public bool SaveSessionReport(  //for saving session report on submit
    int sessionId,
    bool sessionDone,
    string sessionFeedback,
    string resources)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Session SET " +
                    "SessionDone = @SessionDone, " +
                    "SessionFeedback = @SessionFeedback, " +
                    "Resources = @Resources " +
                    "WHERE SessionId = @SessionId", con))
                {
                    cmd.Parameters.AddWithValue("@SessionId", sessionId);
                    cmd.Parameters.AddWithValue("@SessionDone", sessionDone);
                    cmd.Parameters.AddWithValue("@SessionFeedback",
                        (object)sessionFeedback ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Resources",
                        (object)resources ?? DBNull.Value);

                    con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0;
                }
            }
        }
    }
}