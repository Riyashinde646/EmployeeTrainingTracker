using System.Web.Mvc;
using EmployeeTrainingTracker.DAL;

namespace EmployeeTrainingTracker.Controllers
{
    public class AssessmentController : Controller
    {
        private AssessmentDAL assessmentDAL = new AssessmentDAL();

        public ActionResult Index()  //opens the assessment page
        {
            return View();
        }

        public JsonResult GetAssessments() //get assessment data from assessmnet dal
        {
            var assessments = assessmentDAL.GetAssessments();

            return Json(
                assessments,
                JsonRequestBehavior.AllowGet
            );
        }

        [HttpPost]
        public JsonResult CreateAssessments(int scheduleId)  //Create assessment records for this training schedule.
        {
            assessmentDAL.CreateAssessments(scheduleId);

            return Json(new
            {
                success = true,
                message = "Assessments created successfully."
            });
        }

        [HttpPost]
        public JsonResult UpdateAssessment(     //This receives the values entered by the trainer:
            int assessmentId,
            bool assignmentDone,
            bool testConducted,
            int? testMarks,
            string individualFeedback)
        {
            assessmentDAL.UpdateAssessment(
                assessmentId,
                assignmentDone,
                testConducted,
                testMarks,
                individualFeedback
            );

            return Json(new
            {
                success = true,
                message = "Assessment updated successfully."
            });
        }
    }
}