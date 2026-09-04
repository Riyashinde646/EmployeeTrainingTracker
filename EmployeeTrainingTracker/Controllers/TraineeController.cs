using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeTrainingTracker.DAL;
using EmployeeTrainingTracker.Models;
using BCrypt.Net;

namespace EmployeeTrainingTracker.Controllers
{
    public class TraineeController : Controller
    {
        TraineeDAL traineeDAL = new TraineeDAL();


        // getting the list
        public ActionResult Index()
        {
            List<TraineeModel> trainees = traineeDAL.GetTrainees();

            return View(trainees);
        }

        // i used for saving trainee

        [HttpPost]
        public JsonResult SaveTrainee(TraineeModel trainee)
        {
            try
            {
                trainee.Password = BCrypt.Net.BCrypt.HashPassword(trainee.Password);

                traineeDAL.AddTrainee(trainee);

                return Json(new
                {
                    success = true,
                    message = "Trainee added successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        //update status method like active or deactive
        [HttpPost]
        public JsonResult UpdateStatus(int userID, bool isActive)
        {
            try
            {
                traineeDAL.UpdateTraineeStatus(userID, isActive);

                return Json(new
                {
                    success = true,
                    message = isActive ? "Trainee activated." : "Trainee deactivated."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet] //get trainee id and edit
        public JsonResult GetTrainee(int userID)
        {
            try
            {
                TraineeModel trainee = traineeDAL.GetTraineeById(userID);

                return Json(trainee, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost] //saving changes method 
        public JsonResult UpdateTrainee(TraineeModel trainee)
        {
            try
            {
                traineeDAL.UpdateTrainee(trainee);

                return Json(new
                {
                    success = true,
                    message = "Trainee updated successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}