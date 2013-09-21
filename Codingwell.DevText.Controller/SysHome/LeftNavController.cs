using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model;
using Codingwell.DevText.Service;

namespace Codingwell.DevText.Controller.SysHome {
	public class UserNavigationController : AuthBaseController {

		[HttpGet]
		[ChildActionOnly]
		public ActionResult LeftNavigtion ( ) {
			return View();
		}
	}
}
