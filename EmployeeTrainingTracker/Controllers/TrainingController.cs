using System.Web.Mvc;
using EmployeeTrainingTracker.DAL;
using EmployeeTrainingTracker.Models;

namespace EmployeeTrainingTracker.Controllers
{
    public class TrainingController : Controller
    {
        private TrainingDAL trainingDAL = new TrainingDAL();


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OverallTrainingPlan()   // when page opens get overall planning
        {
            var plan = trainingDAL.GetOverallTrainingPlan("", "", "");

            return View(plan);
        }

        public JsonResult GetOverallTrainingPlan(string trainer, string topic, string date)
        {
            var plan = trainingDAL.GetOverallTrainingPlan(trainer, topic, date);

            return Json(plan, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTrainers()
        {
            var trainers = trainingDAL.GetTrainers();

            return Json(trainers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTopics()
        {
            var topics = trainingDAL.GetTopics();

            return Json(topics, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubTopics(int topicId)
        {
            var subTopics = trainingDAL.GetSubTopics(topicId);

            return Json(subTopics, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveTraining(AssignTrainingModel model)
        {
            int scheduleId = trainingDAL.SaveSchedule(new TrainingSchedule
            {
                TrainerId = model.TrainerId,
                TopicId = model.TopicId,
                TrainingDate = model.TrainingDate,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            });

            trainingDAL.SaveScheduleSubTopics(scheduleId, model.SubTopicIds);

            trainingDAL.SaveTrainingTrainees(scheduleId, model.TraineeIds);

            return Json(new
            {
                success = true,
                message = "Training assigned successfully."
            });
        }

        [HttpPost]
        public JsonResult UpdateTraining(AssignTrainingModel model)
        {
            trainingDAL.UpdateTraining(model);

            return Json(new
            {
                success = true,
                message = "Training updated successfully."
            });
        }

        public JsonResult GetTrainingById(int scheduleId)
        {
            var training = trainingDAL.GetTrainingById(scheduleId);

            training.SubTopicIds = trainingDAL.GetSubTopicIds(scheduleId);

            return Json(training, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteTraining(int scheduleId)
        {
            trainingDAL.DeleteTraining(scheduleId);

            return Json(new
            {
                success = true,
                message = "Training deleted successfully."
            });
        }

        public JsonResult GetAllTrainees()
        {
            var trainees = trainingDAL.GetAllTrainees();

            return Json(
                trainees,
                JsonRequestBehavior.AllowGet
            );
        }
    }
}