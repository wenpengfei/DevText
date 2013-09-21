using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.ViewModel;
using Codingwell.DevText.Service;

namespace Codingwell.DevText.Controller {
	public class BlogsController : BaseController {
		private readonly IArticleService articleService;
		private readonly IUserService userService;

		public BlogsController ( IArticleService articleService,IUserService userService ) {
			this.articleService = articleService;
			this.userService = userService;
		}


		[HttpGet]
		public ActionResult Index ( int? page ) {
			int _page = page ?? 1;
			return View(articleService.GetArticles(20, _page));
		}

		[HttpGet]
		public ActionResult Detail ( int id, string path ) {
			Article article = articleService.GetArticle(id);
			if (article == null) {
				throw new HttpException(404, "文章不存在");
			}
			if (( !string.IsNullOrEmpty(path) || !string.IsNullOrEmpty(article.Path) ) && !String.Equals(path, article.Path, StringComparison.OrdinalIgnoreCase)) {
				return this.RedirectToActionPermanent("detail", new { id = article.ID, path = article.Path });
			}
			articleService.UpdateViewCount(id);
			ArticleDetail articleDetail = new ArticleDetail();
			articleDetail.Article = article;
			articleDetail.Owner = userService.GetUser(article.CreatorID);
			return View(articleDetail);
		}

		[HttpGet]
		public ActionResult Category ( int id, int? page ) {
			//ArticleCategoryEntity cate = BLLFactory<NewsCategoryBLL>.Instance.GetCategory(id);
			//if (cate == null)
			//    throw new HttpException(404, "文章分类不存在");
			//ViewData["Category"] = cate;
			//int _page = page ?? 1;
			//ViewData["Page"] = _page;
			//ViewData["Total"] = ( BLLFactory<NewsBLL>.Instance.GetNewsByCategory(id) / 20 ) + 1;
			//ViewData["Articles"] = BLLFactory<NewsBLL>.Instance.GetNewsByCategory(20, _page, id);
			return View();
		}
	}
}
