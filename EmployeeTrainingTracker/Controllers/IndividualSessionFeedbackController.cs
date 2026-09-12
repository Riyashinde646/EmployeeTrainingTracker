using System.Web.Mvc;
using EmployeeTrainingTracker.DAL;

namespace EmployeeTrainingTracker.Controllers
{
    public class IndividualSessionFeedbackController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetIndividualFeedback()
        {
            IndividualSessionFeedbackDAL dal =
                new IndividualSessionFeedbackDAL();

            var feedback =
                dal.GetAllIndividualFeedback();

            return Json(
                feedback,
                JsonRequestBehavior.AllowGet
            );
        }
    }
}