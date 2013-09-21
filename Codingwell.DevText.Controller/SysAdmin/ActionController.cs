using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web.Mvc;

using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Service;
using Jayrock.Json.Conversion;

namespace Codingwell.DevText.Controller.SysAdmin {
	public class ActionController : AuthBaseController {

		private readonly IActionService actionService;

		public ActionController ( IActionService actionService) {
			this.actionService = actionService;
		}

		[HttpGet]
		public ActionResult List ( ) {
			return View();
		}

		[HttpGet]
		public JsonResult GetActionList ( ) {
			int pageindex = GetPageIndex();
			int pagesize = GetPageSize();
			IEnumerable<Action> actions = actionService.GetActions(pagesize, pageindex, string.Empty);
			return Json(new {
				recordCount = actions.Count(),
				list = actions.Select(a => new {
					ID = a.ID,
					Name = a.ActionName,
					Value = a.ActionValue,
					PageUrl = a.PageUrl,
					State = Utili.GetStateString(a.State),
					CreateTime = a.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
					Operation = GetOperationString(a)
				})
			}, JsonRequestBehavior.AllowGet);
		}

		private string GetOperationString ( Action action ) {
			StringBuilder strBul = new StringBuilder();
			//if (BLLFactory<PermissionBLL>.Instance.HasActionPermission(UserContext.Instance.UserID, ActionsDefine.角色动作_动作编辑)) {
			strBul.Append("<a href=\"javascript:EditAction(" + action.ID + ");\"  class=\"blue\">编辑</a>");
			//}
			//if (BLLFactory<PermissionBLL>.Instance.HasActionPermission(UserContext.Instance.UserID, ActionsDefine.角色动作_动作删除)) {
			strBul.Append("<a href=\"javascript:EditAction(" + action.ID + ");\"  class=\"blue\">停用</a>");
			//}
			return strBul.ToString();
		}

		[HttpGet]
		public ActionResult Create ( ) {
			var channels = actionService.GetActionChannels(1, 1, string.Empty);
			var items = channels.Select(c => ( new { Name = c.Name, ID = c.ID.ToString() } ));
			ViewData["ddlChannel"] = new SelectList(items, "ID", "Name");
			return View();
		}
		[HttpGet]
		public ActionResult Edit ( int id ) {
			Action action = actionService.GetAction(id);
			if (action != null) {
				var channels = actionService.GetAllActionChannels();
				var items = channels.Select(c => ( new { Name = c.Name, ID = c.ID.ToString() } ));
				ViewData["ddlChannel"] = new SelectList(items, "ID", "Name", (int)action.ChannelID);
				ViewData["ActionName"] = action.ActionName;
				ViewData["ActionValue"] = action.ActionValue;
				ViewData["PageUrl"] = action.PageUrl;
			}
			return View();
		}

		[HttpPost]
		public JsonResult SubmitCreate ( string json ) {
			try {
				Action action = JsonConvert.Import<Action>(json);
				if (action == null)
					return Json(new { result = false, message = "数据传输格式不正确，请检查后再试" });
				bool result = false;
				if (action.ID == 0) {
					action.CreateTime = System.DateTime.Now;
					action.State = (int)RecordState.Approved;
					result = actionService.CreateAction(action);
					return Json(new { result = result, message = result ? "保存成功" : "保存失败" });
				} else {
					action.State = (int)RecordState.Approved;
					result = actionService.UpdateAction(action);
					return Json(new { result = result, message = result ? "保存成功" : "保存失败" });
				}
			} catch {
				return Json(new { result = false, message = "发生未知错误，请稍候再试" });
			}
		}
	}
}
