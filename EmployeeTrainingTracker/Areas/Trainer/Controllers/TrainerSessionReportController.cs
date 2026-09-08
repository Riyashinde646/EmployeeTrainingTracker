using System;
using System.Web.Mvc;
using EmployeeTrainingTracker.DAL;
using System.IO;
using System.Web;

namespace EmployeeTrainingTracker.Areas.Trainer.Controllers
{
    public class TrainerSessionReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSessionReports()
        {
            int trainerId = Convert.ToInt32(Session["TrainerId"]);

            SessionReportDAL dal = new SessionReportDAL();

            var reports = dal.GetTrainerSessionReports(trainerId);

            return Json(
                reports,
                JsonRequestBehavior.AllowGet
            );
        }

        [HttpPost]
        public JsonResult SaveSessionReport(int sessionId, bool sessionDone, string sessionFeedback, HttpPostedFileBase sessionResource)
        {
            string resources = null;

            if (sessionResource != null && sessionResource.ContentLength > 0)
            {
                string extension = Path.GetExtension(sessionResource.FileName).ToLower();

                if (extension != ".pdf" && extension != ".doc" && extension != ".docx")
                {
                    return Json(new
                    {
                        success = false,
                        message = "Only PDF, DOC and DOCX files are allowed."
                    });
                }

                string folderPath = Server.MapPath("~/Uploads/SessionResources/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Guid.NewGuid().ToString() + extension;

                string filePath = Path.Combine(folderPath, fileName);

                sessionResource.SaveAs(filePath);

                resources = "/Uploads/SessionResources/" + fileName;
            }

            SessionReportDAL dal = new SessionReportDAL();

            bool result = dal.SaveSessionReport(
                sessionId,
                sessionDone,
                sessionFeedback,
                resources
            );

            return Json(new
            {
                success = result,
                message = result
                    ? "Session report saved successfully."
                    : "Unable to save session report."
            });
        }
    }
}