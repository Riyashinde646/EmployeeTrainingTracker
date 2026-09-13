using System.Web.Mvc;
using EmployeeTrainingTracker.Models;
using EmployeeTrainingTracker.DAL;

namespace EmployeeTrainingTracker.Controllers
{
    public class LoginController : Controller
    {
        private TrainerDAL trainerDAL = new TrainerDAL();
        private TraineeDAL traineeDAL = new TraineeDAL();
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

            // Manager login
            string managerEmail = "manager@intellisource.com";
            string managerPassword = "Manager@123";

            if (model.Email == managerEmail &&
                model.Password == managerPassword)
            {
                Session["Email"] = model.Email;
                Session["Role"] = "Manager";

                return RedirectToAction("Index", "ManagerDashboard");
            }

            // Trainer login
            TrainerModel trainer = trainerDAL.GetTrainerForLogin(model.Email);

            if (trainer != null)
            {
                if (!trainer.IsActive)
                {
                    ViewBag.Error = "Your account is inactive.";
                    return View("Index", model);
                }

                bool passwordCorrect =
                    BCrypt.Net.BCrypt.Verify(model.Password, trainer.Password);

                if (passwordCorrect)
                {
                    Session["UserId"] = trainer.UserID;
                    Session["TrainerId"] = trainer.TrainerID;
                    Session["TrainerName"] = trainer.TrainerName;
                    Session["Email"] = trainer.Email;
                    Session["Role"] = "Trainer";

                    return RedirectToAction(
                        "Index",
                        "TrainerDashboard",
                        new { area = "Trainer" } //makes sure it goes to your Trainer Area, not the Manager side.
                    );
                }
            }


            // Trainee login
            TraineeModel trainee = traineeDAL.GetTraineeForLogin(model.Email);

            if (trainee != null)
            {
                if (!trainee.IsActive)
                {
                    ViewBag.Error = "Your account is inactive.";
                    return View("Index", model);
                }

                bool passwordCorrect =
                    BCrypt.Net.BCrypt.Verify(model.Password, trainee.Password);

                if (passwordCorrect)
                {
                    Session["UserId"] = trainee.UserID;
                    Session["TraineeId"] = trainee.TraineeID;
                    Session["TraineeName"] = trainee.TraineeName;
                    Session["Email"] = trainee.Email;
                    Session["Role"] = "Trainee";

                    return RedirectToAction(
                        "Index",
                        "TraineeDashboard",
                        new { area = "Trainee" }
                    );
                }
            }

            ViewBag.Error = "Invalid email or password.";

            return View("Index", model);


        }



        // Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "Login");
        }
    }
}