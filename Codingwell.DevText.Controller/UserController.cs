using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Security;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.ViewModel;
using Codingwell.DevText.Data;
using Codingwell.DevText.Service;
using Codingwell.Utility;

namespace Codingwell.DevText.Controller {
	public class UserController : BaseController {

		private readonly IUserService userService;
		private readonly IArticleService articleService;
		private readonly INewsService newsService;

		public UserController ( IUserService userService, IArticleService articleService,INewsService newsService ) {
			this.userService = userService;
			this.articleService = articleService;
			this.newsService = newsService;
		}

		[HttpGet]
		public ActionResult Home ( ) {
			return View();
		}

		[ChildActionOnly]
		public PartialViewResult UserInfo ( int userid ) {
			User user = userService.GetUser(userid);
			if (user == null) {
				return null;
			}
			ViewData["User"] = user;
			return PartialView();
		}

		[HttpGet]
		public ActionResult Questions ( int userid, int? page ) {
			User user = userService.GetUser(userid);
			if (user == null) {
				return null;
			}
			UserHomeModel model = new UserHomeModel();
			model.CurrentUser = user;
			return View(model);
		}

		[HttpGet]
		public ActionResult Blogs ( int userid, int? page ) {
			User user = userService.GetUser(userid);
			if (user == null) {
				return null;
			}
			UserHomeModel model = new UserHomeModel();
			model.CurrentUser = user;
			int _page = page ?? 1;
			model.Articles = articleService.GetArticlesOfUser(userid, 20, _page);
			return View(model);
		}

		[HttpGet]
		public ActionResult News ( int userid, int? page ) {
			User user = userService.GetUser(userid);
			if (user == null) {
				return null;
			}
			UserHomeModel model = new UserHomeModel();
			model.CurrentUser = user;
			int _page = page ?? 1;
			model.News = newsService.GetNewsOfUser(userid, 20, _page);
			return View(model);
		}

		[HttpGet]
		public ActionResult Answers ( int userid ) {
			User user = userService.GetUser(userid);
			if (user == null) {
				return null;
			}
			UserHomeModel model = new UserHomeModel();
			model.CurrentUser = user;
			return View(model);
		}

		[HttpGet]
		public ActionResult Bookmarks ( int userid ) {
			User user = userService.GetUser(userid);
			if (user == null) {
				return null;
			}
			UserHomeModel model = new UserHomeModel();
			model.CurrentUser = user;
			return View(model);
		}
	}
}
