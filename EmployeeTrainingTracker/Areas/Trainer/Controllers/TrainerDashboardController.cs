using System.Web.Mvc;
using System;
using System.Data;
using EmployeeTrainingTracker.DAL;



namespace EmployeeTrainingTracker.Areas.Trainer.Controllers
{
    public class TrainerDashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult MySessions()
        {
            int trainerId = Convert.ToInt32(Session["TrainerId"]);

            TrainerDAL dal = new TrainerDAL();

            DataTable dt = dal.GetTrainerSessions(trainerId);

            return View(dt);
        }

        public ActionResult TrainingPlan()  //get overall training plan
        {
            TrainingDAL dal = new TrainingDAL();

            var plan = dal.GetOverallTrainingPlan("", "", "");

            return View(plan);
        }

        public ActionResult Assessments() //getting all assessmnets
        {
            AssessmentDAL dal = new AssessmentDAL();

            var assessments = dal.GetAssessments();

            return View(assessments);
        }

        public ActionResult GetPendingAssessments()
        {
            int trainerId = Convert.ToInt32(Session["TrainerId"]);

            AssessmentDAL dal = new AssessmentDAL();

            var assessments = dal.GetPendingAssessments(trainerId);

            return Json(assessments, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SubmitAssessment(
    int scheduleId,
    int traineeId,
    bool assignmentDone,
    bool testConducted,
    int? testMarks,
    string individualFeedback)
        {
            AssessmentDAL dal = new AssessmentDAL();

            dal.SubmitAssessment(
                scheduleId,
                traineeId,
                assignmentDone,
                testConducted,
                testMarks,
                individualFeedback);

            return Json(new
            {
                success = true,
                message = "Assessment submitted successfully."
            });
        }
    }
}