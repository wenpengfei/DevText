using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model;

namespace Codingwell.DevText.Controller {
	public class AuthBaseController : BaseController {

		public AuthBaseController ( ) {
			HttpRequest request = System.Web.HttpContext.Current.Request;
			if (!UserContext.Instance.IsAuthenticated || System.Web.HttpContext.Current.Session.Count == 0) {
				System.Web.HttpContext.Current.Response.Write("<script>parent.location.href=\"/site/login?returnurl=" + System.Web.HttpContext.Current.Request.Url.PathAndQuery + "\";</script>");
				System.Web.HttpContext.Current.Response.End();
			} 
		}
	}
}
