using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Service;
using Codingwell.Utility;

namespace Codingwell.DevText.Controller.SysHome {
	public class ChangePwdController : AuthBaseController {
		private readonly IUserService userService;

		public ChangePwdController ( IUserService userService ) {
			this.userService = userService;
		}


		[HttpGet]
		public ActionResult Index ( ) {
			return View();
		}
	}
}
