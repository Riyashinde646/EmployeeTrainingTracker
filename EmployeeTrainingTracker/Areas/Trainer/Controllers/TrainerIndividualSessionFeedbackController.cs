using System;
using System.Web.Mvc;
using EmployeeTrainingTracker.DAL;

namespace EmployeeTrainingTracker.Areas.Trainer.Controllers
{
    public class TrainerIndividualSessionFeedbackController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetIndividualFeedback()
        {
            int trainerId =
                Convert.ToInt32(Session["TrainerId"]);

            IndividualSessionFeedbackDAL dal =
                new IndividualSessionFeedbackDAL();

            var feedback =
                dal.GetTrainerIndividualFeedback(trainerId);

            return Json(
                feedback,
                JsonRequestBehavior.AllowGet
            );
        }


        [HttpPost]
        public JsonResult SaveFeedback(
            int sessionId,
            int traineeId,
            bool sessionCompleted,
            string feedback)
        {
            IndividualSessionFeedbackDAL dal =
                new IndividualSessionFeedbackDAL();

            bool result =
                dal.SaveIndividualFeedback(
                    sessionId,
                    traineeId,
                    sessionCompleted,
                    feedback
                );

            return Json(new
            {
                success = result,
                message = result
                    ? "Individual feedback saved successfully."
                    : "Unable to save feedback."
            });
        }
    }
}