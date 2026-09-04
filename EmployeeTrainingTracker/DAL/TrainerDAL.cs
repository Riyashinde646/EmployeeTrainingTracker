using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EmployeeTrainingTracker.Models;

namespace EmployeeTrainingTracker.DAL
{
    public class TrainerDAL
    {
        private string connectionString =
            ConfigurationManager.ConnectionStrings["EmployeeTrainingTrackerDB"].ConnectionString;


        public void AddTrainer(TrainerModel trainer)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_AddTrainer", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Email", trainer.Email);
                    cmd.Parameters.AddWithValue("@Password", trainer.Password);
                    cmd.Parameters.AddWithValue("@TrainerName", trainer.TrainerName);
                    cmd.Parameters.AddWithValue("@Phone", trainer.Phone ?? "");

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTrainerStatus(int userID, bool isActive)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateUserStatus", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@IsActive", isActive);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public TrainerModel GetTrainerById(int userID)
        {
            TrainerModel trainer = new TrainerModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetTrainerById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    trainer.TrainerID = Convert.ToInt32(reader["TrainerID"]);
                    trainer.UserID = Convert.ToInt32(reader["UserID"]);
                    trainer.TrainerName = reader["TrainerName"].ToString();
                    trainer.Email = reader["Email"].ToString();
                    trainer.Phone = reader["Phone"].ToString();
                    trainer.IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
            }

            return trainer;
        }

        public void EditTrainer(TrainerModel trainer)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_EditTrainer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", trainer.UserID);
                cmd.Parameters.AddWithValue("@TrainerName", trainer.TrainerName);
                cmd.Parameters.AddWithValue("@Email", trainer.Email);
                cmd.Parameters.AddWithValue("@Phone", trainer.Phone);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public List<TrainerModel> GetTrainers()
        {
            List<TrainerModel> trainers = new List<TrainerModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetTrainers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TrainerModel trainer = new TrainerModel();

                            trainer.TrainerID = Convert.ToInt32(reader["TrainerID"]);
                            trainer.UserID = Convert.ToInt32(reader["UserID"]);
                            trainer.TrainerName = reader["TrainerName"].ToString();
                            trainer.Email = reader["Email"].ToString();
                            trainer.Phone = reader["Phone"].ToString();
                            trainer.IsActive = Convert.ToBoolean(reader["IsActive"]);
                            trainer.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);

                            trainers.Add(trainer);
                        }
                    }
                }
            }

            return trainers;
        }
    }
}