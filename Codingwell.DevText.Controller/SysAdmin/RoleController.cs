using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Service;

namespace Codingwell.DevText.Controller.SysAdmin {
	public class RoleController : AuthBaseController {
		private readonly IRoleService roleService;
		private readonly IActionService actionService;

		public RoleController ( IRoleService roleservice,IActionService actionservice ) {
			this.roleService = roleservice;
			this.actionService = actionservice;
		}


		[HttpGet]
		public ActionResult List ( ) {
			return View();
		}

		[HttpGet]
		public JsonResult GetRoleList ( ) {
			int pageindex = GetPageIndex();
			int pagesize = GetPageSize();
			IEnumerable<Role> roles = roleService.GetRoles(pagesize, pageindex, string.Empty);
			return Json(new {
				recordCount = roles.Count(),
				list = roles.Select(r => new {
					ID = r.ID,
					Name = r.Name,
					State = Utili.GetStateString(r.State),
					CreateTime = r.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
					Description = r.Description,
					Operation = ""
				})

			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult Create ( ) {
			IEnumerable<ActionChannel> channels = actionService.GetAllActionChannels();
			ViewData["AllChannels"] = channels;
			return View();
		}
	}
}
