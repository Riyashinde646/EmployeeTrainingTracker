using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeTrainingTracker.Models;

namespace EmployeeTrainingTracker.DAL
{
    public class IndividualSessionFeedbackDAL
    {
        private string connectionString =
            System.Configuration.ConfigurationManager
            .ConnectionStrings["EmployeeTrainingTrackerDB"]
            .ConnectionString;


        // Get trainees for trainer's sessions
        public List<IndividualSessionFeedbackModel> GetTrainerIndividualFeedback(int trainerId)
        {
            List<IndividualSessionFeedbackModel> feedbackList =
                new List<IndividualSessionFeedbackModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_GetTrainerIndividualFeedback", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TrainerId", trainerId);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    IndividualSessionFeedbackModel feedback =
                        new IndividualSessionFeedbackModel();

                    feedback.SessionId =
                        Convert.ToInt32(dr["SessionId"]);

                    feedback.ScheduleId =
                        Convert.ToInt32(dr["ScheduleId"]);

                    feedback.TrainingDate =
                        Convert.ToDateTime(dr["TrainingDate"]);

                    feedback.TopicName =
                        dr["TopicName"].ToString();

                    feedback.SubTopics =
                        dr["SubTopicName"].ToString();

                    feedback.TraineeId =
      Convert.ToInt32(dr["TraineeId"]);

                    feedback.TraineeName =
                        dr["TraineeName"].ToString();

                    feedback.SessionCompleted =
                        Convert.ToBoolean(dr["SessionCompleted"]);

                    feedback.Feedback =
                        dr["Feedback"].ToString();

                    feedback.HasFeedback =
                        Convert.ToBoolean(dr["HasFeedback"]);

                    feedbackList.Add(feedback);
                }
            }

            return feedbackList;
        }


        // Save individual feedback
        public bool SaveIndividualFeedback(
            int sessionId,
            int traineeId,
            bool sessionCompleted,
            string feedback)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_SaveIndividualSessionFeedback", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@TraineeId", traineeId);
                cmd.Parameters.AddWithValue(
                    "@SessionCompleted", sessionCompleted);

                cmd.Parameters.AddWithValue(
                    "@Feedback",
                    (object)feedback ?? DBNull.Value);

                con.Open();

                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }


        // Get all individual feedback
        public List<IndividualSessionFeedbackModel> GetAllIndividualFeedback()
        {
            List<IndividualSessionFeedbackModel> feedbackList =
                new List<IndividualSessionFeedbackModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "sp_GetAllIndividualSessionFeedback", con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    IndividualSessionFeedbackModel feedback =
                        new IndividualSessionFeedbackModel();

                    feedback.FeedbackId =
                        Convert.ToInt32(dr["FeedbackId"]);

                    feedback.TrainerName =
                        dr["TrainerName"].ToString();

                    feedback.TraineeName =
                        dr["TraineeName"].ToString();

                    feedback.TopicName =
                        dr["TopicName"].ToString();

                    feedback.SubTopics =
                        dr["SubTopics"].ToString();

                    feedback.TrainingDate =
                        Convert.ToDateTime(dr["TrainingDate"]);

                    feedback.SessionCompleted =
                        Convert.ToBoolean(dr["SessionCompleted"]);

                    feedback.Feedback =
                        dr["Feedback"].ToString();

                    feedbackList.Add(feedback);
                }
            }

            return feedbackList;
        }
    }
}