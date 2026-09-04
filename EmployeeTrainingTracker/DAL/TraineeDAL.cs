using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EmployeeTrainingTracker.Models;

namespace EmployeeTrainingTracker.DAL
{
    public class TraineeDAL
    {
        private string connectionString =  
            ConfigurationManager.ConnectionStrings["EmployeeTrainingTrackerDB"].ConnectionString;

        public void AddTrainee(TraineeModel trainee)  // add trainee function
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddTrainee", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TraineeName", trainee.TraineeName);
                cmd.Parameters.AddWithValue("@Email", trainee.Email);
                cmd.Parameters.AddWithValue("@Password", trainee.Password);
                cmd.Parameters.AddWithValue("@Phone", trainee.Phone);
                cmd.Parameters.AddWithValue("@Department", trainee.Department); 
                cmd.Parameters.AddWithValue("@Designation", trainee.Designation);
                cmd.Parameters.AddWithValue("@JoiningDate", trainee.JoiningDate);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<TraineeModel> GetTrainees()   //get trainee function 
        {
            List<TraineeModel> trainees = new List<TraineeModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetTrainees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TraineeModel trainee = new TraineeModel();

                    trainee.TraineeID = Convert.ToInt32(reader["TraineeID"]);
                    trainee.UserID = Convert.ToInt32(reader["UserID"]);
                    trainee.TraineeName = reader["TraineeName"].ToString();
                    trainee.Email = reader["Email"].ToString();
                    trainee.Phone = reader["Phone"].ToString();
                    trainee.Department = reader["Department"].ToString();
                    trainee.Designation = reader["Designation"].ToString();
                    trainee.JoiningDate = Convert.ToDateTime(reader["JoiningDate"]);
                    trainee.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    trainee.IsActive = Convert.ToBoolean(reader["IsActive"]);

                    trainees.Add(trainee);
                }
            }

            return trainees;
        }

        public void UpdateTraineeStatus(int userID, bool isActive)    //update status like active , deactive
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

        public TraineeModel GetTraineeById(int userID) // get trainee by id for editing
        {
            TraineeModel trainee = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetTraineeById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    trainee = new TraineeModel();

                    trainee.TraineeID = Convert.ToInt32(reader["TraineeID"]);
                    trainee.UserID = Convert.ToInt32(reader["UserID"]);
                    trainee.TraineeName = reader["TraineeName"].ToString();
                    trainee.Email = reader["Email"].ToString();
                    trainee.Phone = reader["Phone"].ToString();
                    trainee.Department = reader["Department"].ToString();
                    trainee.Designation = reader["Designation"].ToString();
                    trainee.JoiningDate = Convert.ToDateTime(reader["JoiningDate"]);
                    trainee.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    trainee.IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
            }

            return trainee;
        }

        // update trainnee that is saving changes function
        public void UpdateTrainee(TraineeModel trainee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateTrainee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", trainee.UserID);
                cmd.Parameters.AddWithValue("@TraineeName", trainee.TraineeName);
                cmd.Parameters.AddWithValue("@Phone", trainee.Phone);
                cmd.Parameters.AddWithValue("@Department", trainee.Department);
                cmd.Parameters.AddWithValue("@Designation", trainee.Designation);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

