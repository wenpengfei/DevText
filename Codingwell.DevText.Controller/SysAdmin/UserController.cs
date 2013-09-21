using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Service;

namespace Codingwell.DevText.Controller.SysAdmin {
	public class UserController : AuthBaseController {
		private readonly IUserService userService;

		public UserController ( IUserService userService ) {
			this.userService = userService;
		}


		[HttpGet]
		public ActionResult List ( ) {
			return View();
		}

		[HttpGet]
		public JsonResult GetUserList ( ) {
			int pageindex = GetPageIndex();
			int pagesize = GetPageSize();
			IEnumerable<User> users = userService.GetUsers(pagesize, pageindex, null);
			return Json(new {
				recordCount = users.Count(),
				list = users.Select(u => new {
					ID = u.UserID,
					InsertTime = u.CreateTime.ToString("yyyy-MM-dd HH:mm"),
					NickName = u.NickName,
					State = Utili.GetAccountStateString(u.AccountState),
					LastLoginTime = u.LastLoginTime.ToString("yyyy-MM-dd HH:mm"),
					LastLoginIp = u.LastLoginIP,
					Operation = GetUserOperation(u)
				})
			}, JsonRequestBehavior.AllowGet);
		}

		private string GetUserOperation ( User user ) {
			StringBuilder strBul = new StringBuilder();
			//if (BLLFactory<PermissionBLL>.Instance.HasActionPermission(UserContext.Instance.UserID, SysActions.用户_编辑)) {
				strBul.Append("<a href=\"/sysadmin/user/edit/" + user.UserID + "\">编辑</a>");
			//}
			//if (BLLFactory<PermissionBLL>.Instance.HasActionPermission(UserContext.Instance.UserID, SysActions.用户_停用)) {
				strBul.Append("<a href=\"/sysadmin/user/edit/" + user.UserID + "\">停用</a>");
			//}
			return strBul.ToString();
		}

		public ActionResult Create ( ) {
			return View();
		}
	}
}
