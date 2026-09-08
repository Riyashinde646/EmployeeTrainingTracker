using System.Web.Mvc;

namespace EmployeeTrainingTracker.Areas.Trainee
{
    public class TraineeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Trainee";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Trainee_default",
                "Trainee/TraineeDashboard/{action}/{id}",
                new
                {
                    controller = "TraineeDashboard",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}