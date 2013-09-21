using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model;
using Codingwell.DevText.Service;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Controller.SysHome {
	public class BookmarkController : AuthBaseController {
		private readonly IBookmarkService bookmarkService;

		public BookmarkController ( IBookmarkService bookmarkService ) {
			this.bookmarkService = bookmarkService;
		}

		[HttpGet]
		public ActionResult Index ( int? page ) {
			int _page = page ?? 1;
			return View(bookmarkService.GetBookmarksOfUser(20, _page, UserContext.Instance.UserID));
		}
	}
}
