using System;
using System.Web.Mvc;
using EmployeeTrainingTracker.DAL;
using EmployeeTrainingTracker.Models;

namespace EmployeeTrainingTracker.Controllers
{
    public class TrainerController : Controller
    {
        private TrainerDAL trainerDAL = new TrainerDAL();

        // i used this to get trainers list
        public ActionResult Index()
        {
            var trainers = trainerDAL.GetTrainers();

            return View(trainers);
        }

        // addding Trainer page
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Save(TrainerModel trainer)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Please enter valid details."
                });
            }

            try
            {
                trainer.Password = BCrypt.Net.BCrypt.HashPassword(trainer.Password);

                trainerDAL.AddTrainer(trainer);

                return Json(new
                {
                    success = true,
                    message = "Trainer added successfully."
                });
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to add trainer."
                });
            }
        }

        //updating status method
        [HttpPost]
        public JsonResult UpdateStatus(int userID, bool isActive)
        {
            try
            {
                trainerDAL.UpdateTrainerStatus(userID, isActive);

                return Json(new
                {
                    success = true,
                    message = isActive ? "Trainer activated." : "Trainer deactivated."
                });
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to update trainer."
                });
            }
        }

        // here we are editing the trainer model 

        [HttpPost]
        public JsonResult EditTrainer(TrainerModel trainer)
        {
            try
            {
                trainerDAL.EditTrainer(trainer);

                return Json(new
                {
                    success = true,
                    message = "Trainer updated successfully."
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

        //getting trainer 
        public JsonResult GetTrainer(int userID)
        {
            try
            {
                TrainerModel trainer = trainerDAL.GetTrainerById(userID);

                return Json(trainer, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}