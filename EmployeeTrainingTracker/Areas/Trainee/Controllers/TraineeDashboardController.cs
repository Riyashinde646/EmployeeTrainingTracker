using System;
using System.Web.Mvc;
using EmployeeTrainingTracker.DAL;

namespace EmployeeTrainingTracker.Areas.Trainee.Controllers
{
    public class TraineeDashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult TrainingPlanning()
        {
            TrainingDAL dal = new TrainingDAL();

            var plan =
                dal.GetOverallTrainingPlan("", "", "");

            return View(plan);
        }


        public ActionResult MyMarks()
        {
            int traineeId =
                Convert.ToInt32(Session["TraineeId"]);

            AssessmentDAL dal =
                new AssessmentDAL();

            var marks =
                dal.GetMyMarks(traineeId);

            return View(marks);
        }

        public ActionResult Resources()
        {
            SessionReportDAL dal = new SessionReportDAL();

            var resources = dal.GetAllSessionResources();

            return View(resources);
        }
    }
}