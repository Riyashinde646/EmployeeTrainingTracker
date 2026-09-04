using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EmployeeTrainingTracker.Models;

namespace EmployeeTrainingTracker.DAL
{
    public class TrainingDAL
    {
        string connectionString =
            ConfigurationManager.ConnectionStrings["EmployeeTrainingTrackerDB"].ConnectionString;


        public List<TrainerModel> GetTrainers()
        {
            List<TrainerModel> trainers = new List<TrainerModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetTrainers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TrainerModel trainer = new TrainerModel();

                    trainer.TrainerID = Convert.ToInt32(dr["TrainerID"]);
                    trainer.TrainerName = dr["TrainerName"].ToString();

                    trainers.Add(trainer);
                }
            }

            return trainers;
        }


        public List<Topic> GetTopics()
        {
            List<Topic> topics = new List<Topic>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetTopics", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Topic topic = new Topic();

                    topic.TopicId = Convert.ToInt32(dr["TopicId"]);
                    topic.TopicName = dr["TopicName"].ToString();

                    topics.Add(topic);
                }
            }

            return topics;
        }


        public List<SubTopic> GetSubTopics(int topicId)
        {
            List<SubTopic> subTopics = new List<SubTopic>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetSubTopics", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TopicId", topicId);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    SubTopic subTopic = new SubTopic();

                    subTopic.SubTopicId = Convert.ToInt32(dr["SubTopicId"]);
                    subTopic.TopicId = Convert.ToInt32(dr["TopicId"]);
                    subTopic.SubTopicName = dr["SubTopicName"].ToString();

                    subTopics.Add(subTopic);
                }
            }

            return subTopics;
        }


        public int SaveSchedule(TrainingSchedule schedule)
        {
            int scheduleId = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SaveSchedule", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TrainerId", schedule.TrainerId);
                cmd.Parameters.AddWithValue("@TopicId", schedule.TopicId);
                cmd.Parameters.AddWithValue("@TrainingDate", schedule.TrainingDate);
                cmd.Parameters.AddWithValue("@StartTime", schedule.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", schedule.EndTime);

                con.Open();

                scheduleId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return scheduleId;
        }


        public void SaveScheduleSubTopics(int scheduleId, List<int> subTopicIds)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                foreach (int subTopicId in subTopicIds)
                {
                    SqlCommand cmd = new SqlCommand(
                        "sp_SaveScheduleSubTopic", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ScheduleId", scheduleId);
                    cmd.Parameters.AddWithValue("@SubTopicId", subTopicId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<TrainingPlanViewModel> GetOverallTrainingPlan(
            string trainer,
            string topic,
            string date)
        {
            List<TrainingPlanViewModel> plan =
                new List<TrainingPlanViewModel>();

            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand("sp_GetOverallTrainingPlan", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Trainer", trainer ?? "");
                cmd.Parameters.AddWithValue("@Topic", topic ?? "");
                cmd.Parameters.AddWithValue("@Date", date ?? "");

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TrainingPlanViewModel training =
                        new TrainingPlanViewModel();

                    training.ScheduleId =
                        Convert.ToInt32(dr["ScheduleId"]);

                    training.TrainerName =
                        dr["TrainerName"].ToString();

                    training.TopicName =
                        dr["TopicName"].ToString();

                    training.SubTopicName =
                        dr["SubTopicName"].ToString();

                    training.TrainingDate =
                        Convert.ToDateTime(dr["TrainingDate"]);

                    training.StartTime =
                        (TimeSpan)dr["StartTime"];

                    training.EndTime =
                        (TimeSpan)dr["EndTime"];

                    plan.Add(training);
                }
            }

            return plan;
        }


        public AssignTrainingModel GetTrainingById(int scheduleId)
        {
            AssignTrainingModel training =
                new AssignTrainingModel();

            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand("sp_GetTrainingById", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ScheduleId", scheduleId);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    training.TrainerId =
                        Convert.ToInt32(dr["TrainerId"]);

                    training.TopicId =
                        Convert.ToInt32(dr["TopicId"]);

                    training.TrainingDate =
                        Convert.ToDateTime(dr["TrainingDate"]);

                    training.StartTime =
                        (TimeSpan)dr["StartTime"];

                    training.EndTime =
                        (TimeSpan)dr["EndTime"];
                }
            }

            return training;
        }


        public List<int> GetSubTopicIds(int scheduleId)
        {
            List<int> subTopicIds = new List<int>();

            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand("sp_GetSubTopicIds", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ScheduleId", scheduleId);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    subTopicIds.Add(
                        Convert.ToInt32(dr["SubTopicId"]));
                }
            }

            return subTopicIds;
        }


        public void UpdateTraining(AssignTrainingModel model)
        {
            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                con.Open();

                // Update TrainingSchedule

                SqlCommand cmd =
                    new SqlCommand("sp_UpdateTraining", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@ScheduleId", model.ScheduleId);

                cmd.Parameters.AddWithValue(
                    "@TrainerId", model.TrainerId);

                cmd.Parameters.AddWithValue(
                    "@TopicId", model.TopicId);

                cmd.Parameters.AddWithValue(
                    "@TrainingDate", model.TrainingDate);

                cmd.Parameters.AddWithValue(
                    "@StartTime", model.StartTime);

                cmd.Parameters.AddWithValue(
                    "@EndTime", model.EndTime);

                cmd.ExecuteNonQuery();


                // Remove old SubTopics

                SqlCommand deleteCmd =
                    new SqlCommand(
                        "sp_DeleteScheduleSubTopics", con);

                deleteCmd.CommandType =
                    CommandType.StoredProcedure;

                deleteCmd.Parameters.AddWithValue(
                    "@ScheduleId", model.ScheduleId);

                deleteCmd.ExecuteNonQuery();


                // Insert new SubTopics

                foreach (int subTopicId in model.SubTopicIds)
                {
                    SqlCommand insertCmd =
                        new SqlCommand(
                            "sp_InsertScheduleSubTopic", con);

                    insertCmd.CommandType =
                        CommandType.StoredProcedure;

                    insertCmd.Parameters.AddWithValue(
                        "@ScheduleId", model.ScheduleId);

                    insertCmd.Parameters.AddWithValue(
                        "@SubTopicId", subTopicId);

                    insertCmd.ExecuteNonQuery();
                }
            }
        }


        public void DeleteTraining(int scheduleId)
        {
            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "sp_DeleteTraining", con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@ScheduleId", scheduleId);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void SaveTrainingTrainees(int scheduleId, List<int> traineeIds) //Connect multiple trainees to one training schedule and save those connections in TrainingTrainee.
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                foreach (int traineeId in traineeIds)
                {
                    SqlCommand cmd = new SqlCommand(
                        "sp_SaveTrainingTrainee", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(
                        "@ScheduleId", scheduleId);

                    cmd.Parameters.AddWithValue(
                        "@TraineeId", traineeId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<TraineeModel> GetAllTrainees() //
        {
            List<TraineeModel> trainees =
                new List<TraineeModel>();

            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand("sp_GetAllTrainees", con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                con.Open();

                SqlDataReader dr =
                    cmd.ExecuteReader();

                while (dr.Read())
                {
                    TraineeModel trainee =
                        new TraineeModel();

                    trainee.TraineeID =
                        Convert.ToInt32(dr["TraineeID"]);

                    trainee.TraineeName =
                        dr["TraineeName"].ToString();

                    trainees.Add(trainee);
                }
            }

            return trainees;
        }
    }
}