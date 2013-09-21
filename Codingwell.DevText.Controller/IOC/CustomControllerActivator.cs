using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Codingwell.DevText.Controller.IOC {
	public class CustomControllerActivator : IControllerActivator {
		IController IControllerActivator.Create (
			System.Web.Routing.RequestContext requestContext,
			Type controllerType ) {
			return DependencyResolver.Current
				.GetService(controllerType) as IController;
		}
	}
}
