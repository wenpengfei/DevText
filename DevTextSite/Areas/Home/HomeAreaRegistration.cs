using System.Web.Mvc;

namespace DevTextSite.Areas.Home {
	public class HomeAreaRegistration : AreaRegistration {
		public override string AreaName {
			get {
				return "Home";
			}
		}

		public override void RegisterArea ( AreaRegistrationContext context ) {
			context.MapRoute(
				"Home_default",
				"home/{controller}/{action}/{id}",
				new { controller = "Event", action = "Index", id = UrlParameter.Optional },
				new string[] { "Codingwell.DevText.Controller.SysHome" }
			);
		}
	}
}
