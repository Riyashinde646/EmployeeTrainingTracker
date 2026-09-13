using System.Web.Mvc;
using EmployeeTrainingTracker.DAL;

namespace EmployeeTrainingTracker.Controllers
{
    public class ManagerDashboardController : Controller
    {
        public ActionResult Index()
        {
            ManagerDashboardDAL dal =
                new ManagerDashboardDAL();

            var dashboard =
                dal.GetDashboardCounts();

            return View(dashboard);
        }
    }
}