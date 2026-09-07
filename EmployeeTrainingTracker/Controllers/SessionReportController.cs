using System.Web.Mvc;
using EmployeeTrainingTracker.DAL;

namespace EmployeeTrainingTracker.Controllers
{
    public class SessionReportController : Controller
    {
        private SessionReportDAL sessionReportDAL =
            new SessionReportDAL();

        public ActionResult Index() //opens the session eport page
        {
            return View();
        }

        public JsonResult GetSessionReports() //this gets the actual data
        {
            var reports =
                sessionReportDAL.GetSessionReports();

            return Json(
                reports,
                JsonRequestBehavior.AllowGet
            );
        }
    }
}