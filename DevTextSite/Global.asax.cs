using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using MvcSiteMapProvider.Web;

namespace DevTextSite {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication {
		ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public static void RegisterGlobalFilters ( GlobalFilterCollection filters ) {
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes ( RouteCollection routes ) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
			routes.MapRoute(
				"sitemap",
				"sitemap.xml",
				new { controller = "Site", action = "SiteMapXml" },
				new string[] { "Codingwell.DevText.Controller" }
				);

			routes.MapRoute(
				"NewsDetail", // Route name
				"news/detail/{id}/{*path}", // URL with parameters
				new { controller = "News", action = "Detail", id = (string)null, path = (string)null },
				new { id = "[0-9]+", path = "[a-zA-Z0-9\\-]*" },
				new string[] { "Codingwell.DevText.Controller" }
			);

			routes.MapRoute(
				"BlogsDetail", // Route name
				"blogs/detail/{id}/{*path}", // URL with parameters
				new { controller = "Blogs", action = "Detail", id = (string)null, path = (string)null },
				new { id = "[0-9]+", path = "[a-zA-Z0-9\\-]*" },
				new string[] { "Codingwell.DevText.Controller" }
			);
			routes.MapRoute(
				"UserInfo", // Route name
				"user/{userid}/{action}", // URL with parameters
				new { controller = "User", action = "Blogs", id = (string)null, path = (string)null },
				new { userid = "[0-9]+" },
				new string[] { "Codingwell.DevText.Controller" }
			);

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Site", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
				new string[] { "Codingwell.DevText.Controller" }
			);
		}

		protected void Application_Start ( ) {
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new RazorViewEngine());
			AreaRegistration.RegisterAllAreas();
			// Register XmlSiteMapController
			XmlSiteMapController.RegisterRoutes(RouteTable.Routes);
			GlobalFilters.Filters.Add(new Codingwell.DevText.Controller.Utilis.RemoveDuplicateContentAttribute());
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
			log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Server.MapPath("~") + @"\log4net.config"));

			Codingwell.DevText.Controller.BootStrapper.Initialise();
		}

		protected void Application_Error ( object sender, EventArgs e ) {
			//Exception unhandledException = Server.GetLastError();
			//Server.ClearError();
			//log.Error(unhandledException.Message, unhandledException);
			//if (Codingwell.DevText.Controller.UserContext.Instance.IsAuthenticated) {
			//    RouteData routeData = new RouteData();
			//    routeData.Values.Add("controller", "Site");
			//    routeData.Values.Add("action", "Error");
			//    //Call target Controller and pass the routeData.
			//    IController errorController = new Codingwell.DevText.Controller.SiteController();
			//    errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
			//} else {
			//    if (unhandledException.GetBaseException() is HttpException) {//http异常
			//        Response.Clear();
			//        Response.Redirect("/");
			//    } else {
			//        Response.Clear();
			//        Response.Redirect("/site/login?ReturnUrl=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
			//    }
			//}
		}


	}
}