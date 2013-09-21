using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model;

namespace Codingwell.DevText.Controller.SysAdmin {
	public class SysNavigationController : AuthBaseController {

		[HttpGet]
		[ChildActionOnly]
		public PartialViewResult LeftNavigation ( ) {
			return PartialView();
		}
	}
}
