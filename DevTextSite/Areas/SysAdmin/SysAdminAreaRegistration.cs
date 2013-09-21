using System.Web.Mvc;

namespace DevTextSite.Areas.SysAdmin {
	public class SysAdminAreaRegistration : AreaRegistration {
		public override string AreaName {
			get {
				return "SysAdmin";
			}
		}

		public override void RegisterArea ( AreaRegistrationContext context ) {
			context.MapRoute(
				"SysAdmin_default",
				"sysadmin/{controller}/{action}/{id}",
				new { controller = "User", action = "List", id = UrlParameter.Optional },
				new string[] { "Codingwell.DevText.Controller.SysAdmin" }
			);
		}
	}
}
