using System.Web.Mvc;

namespace EmployeeTrainingTracker.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            if (Session["Role"] == null ||
                Session["Role"].ToString() != "Manager")
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}