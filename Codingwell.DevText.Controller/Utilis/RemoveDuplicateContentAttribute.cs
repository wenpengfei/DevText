﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Codingwell.DevText.Controller.Utilis {
	public class RemoveDuplicateContentAttribute : ActionFilterAttribute {
		public override void OnActionExecuting ( ActionExecutingContext filterContext ) {
			if (!filterContext.IsChildAction) {
				var routes = RouteTable.Routes;
				var requestContext = filterContext.RequestContext;
				var routeData = requestContext.RouteData;
				var dataTokens = routeData.DataTokens;
				if (dataTokens["area"] == null)
					dataTokens.Add("area", "");
				var vpd = routes.GetVirtualPathForArea(requestContext, routeData.Values);
				if (vpd != null) {
					var virtualPath = vpd.VirtualPath.ToLower();
					var request = requestContext.HttpContext.Request;
					if (!string.Equals(virtualPath, request.Path)) {
						filterContext.Result = new RedirectResult(virtualPath + request.Url.Query, true);
					}
				}
				base.OnActionExecuting(filterContext);
			}
		}
	}
}
