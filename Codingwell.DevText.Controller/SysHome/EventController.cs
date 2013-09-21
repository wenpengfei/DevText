using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model;
using Codingwell.DevText.Service;

namespace Codingwell.DevText.Controller.SysHome {
	public class EventController : AuthBaseController {

		[HttpGet]
		public ActionResult Index ( ) {
			return View();
		}

		[HttpGet]
		public ActionResult Blog ( ) {
			return View();
		}

		[HttpGet]
		public ActionResult Question ( ) {
			return View();
		}

		[HttpGet]
		public ActionResult News ( ) {
			return View();
		}


	}
}
