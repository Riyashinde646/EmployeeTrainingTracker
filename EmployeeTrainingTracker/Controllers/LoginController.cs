using System.Web.Mvc;
using EmployeeTrainingTracker.Models;

namespace EmployeeTrainingTracker.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            // hardcoded manager details
            string managerEmail = "manager@intellisource.com";
            string managerPassword = "Manager@123";

            if (model.Email == managerEmail &&
                model.Password == managerPassword)
            {
                Session["Email"] = model.Email;
                Session["Role"] = "Manager";

                return RedirectToAction("Index", "Manager");
            }

            ViewBag.Error = "Invalid email or password.";

            return View("Index", model);
        }

        // Logout
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}