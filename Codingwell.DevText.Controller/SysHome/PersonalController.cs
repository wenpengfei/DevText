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
	public class PersonalController : AuthBaseController {

		[HttpGet]
		public ActionResult Index ( ) {
			return View();
		}

		[HttpGet]
		public ActionResult Contact ( ) {
			return View();
		}

		[HttpGet]
		public ActionResult Sign ( ) {
			return View();
		}

		[HttpGet]
		public ActionResult Skill ( ) {
			return View();
		}

		[HttpGet]
		public ActionResult ChangePwd ( ) {
			return View();
		}
	}
}
