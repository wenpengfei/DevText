﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model;

namespace Codingwell.DevText.Controller {

	public class SoftwareController : BaseController {

		[HttpGet]
		public ActionResult Index ( ) {
			return View();
		}


	}
}
