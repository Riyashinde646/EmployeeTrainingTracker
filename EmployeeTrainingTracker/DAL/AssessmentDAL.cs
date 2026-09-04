using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EmployeeTrainingTracker.Models;

namespace EmployeeTrainingTracker.DAL
{
    public class AssessmentDAL
    {
        string connectionString =
            ConfigurationManager.ConnectionStrings["EmployeeTrainingTrackerDB"].ConnectionString;

        public List<AssessmentViewModel> GetAssessments()   //Gets all assessment records to display in the grid.
        {
            List<AssessmentViewModel> assessments =
                new List<AssessmentViewModel>();

            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand("sp_GetAssessments", con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    AssessmentViewModel assessment =
                        new AssessmentViewModel();

                    assessment.AssessmentId =
                        Convert.ToInt32(dr["AssessmentId"]);

                    assessment.ScheduleId =
                        Convert.ToInt32(dr["ScheduleId"]);

                    assessment.TraineeId =
                        Convert.ToInt32(dr["TraineeId"]);

                    assessment.SubTopicId =
                        Convert.ToInt32(dr["SubTopicId"]);

                    assessment.TraineeName =
                        dr["TraineeName"].ToString();

                    assessment.TopicName =
                        dr["TopicName"].ToString();

                    assessment.SubTopicName =
                        dr["SubTopicName"].ToString();

                    assessment.AssignmentDone =
                        Convert.ToBoolean(dr["AssignmentDone"]);

                    assessment.TestConducted =
                        Convert.ToBoolean(dr["TestConducted"]);

                    if (dr["TestMarks"] != DBNull.Value)
                    {
                        assessment.TestMarks =
                            Convert.ToInt32(dr["TestMarks"]);
                    }

                    assessment.IndividualFeedback =
                        dr["IndividualFeedback"].ToString();

                    assessments.Add(assessment);
                }
            }

            return assessments;
        }

        public void CreateAssessments(int scheduleId) //Creates the assessment rows for a particular training schedule.
        {
            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand("sp_CreateAssessments", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@ScheduleId", scheduleId);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateAssessment(  //Saves the trainer's assessment information.
            int assessmentId,
            bool assignmentDone,
            bool testConducted,
            int? testMarks,
            string individualFeedback)
        {
            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand("sp_UpdateAssessment", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@AssessmentId", assessmentId);

                cmd.Parameters.AddWithValue(
                    "@AssignmentDone", assignmentDone);

                cmd.Parameters.AddWithValue(
                    "@TestConducted", testConducted);

                if (testMarks.HasValue)
                {
                    cmd.Parameters.AddWithValue(
                        "@TestMarks", testMarks.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(
                        "@TestMarks", DBNull.Value);
                }

                cmd.Parameters.AddWithValue(
                    "@IndividualFeedback",
                    individualFeedback ?? "");

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}