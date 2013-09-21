using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Security;
using System.Configuration;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Service;
using Codingwell.Utility;

namespace Codingwell.DevText.Controller {
	public class SharedController : BaseController {

		private readonly INewsService newsService;
		private readonly IArticleService articleService;
		public SharedController ( INewsService newsService,IArticleService articleService ) {
			this.newsService = newsService;
			this.articleService = articleService;
		}

		[HttpGet]
		[ChildActionOnly]
		[OutputCache(Duration=10)]
		public PartialViewResult HotestNews ( ) {
			return PartialView(newsService.GetNewsByViewCountDesc(5));
		}

		[HttpGet]
		[ChildActionOnly]
		[OutputCache(Duration = 10)]
		public PartialViewResult LatestNews ( ) {
			//ViewData["LatestNews"] = newsService.GetNewsByTimeDesc(13).ToList();
			return PartialView(newsService.GetNewsByTimeDesc(13).ToList());
		}

		[HttpGet]
		[ChildActionOnly]
		[OutputCache(Duration = 10)]
		public PartialViewResult UpestNews ( ) {
			//ViewData["UpestNews"] = newsService.GetNewsByUpCountDesc(10);
			return PartialView(newsService.GetNewsByUpCountDesc(10));
		}

		[HttpGet]
		[ChildActionOnly]
		[OutputCache(Duration = 10)]
		public PartialViewResult LatestBlogs ( ) {
			//ViewData["LatestBlogs"] = articleService.GetArticlesByTimeDesc(13).ToList();
			return PartialView(articleService.GetArticlesByTimeDesc(13).ToList());
		}

		[HttpGet]
		[ChildActionOnly]
		[OutputCache(Duration = 10)]
		public PartialViewResult HotestBlogs ( ) {
			//ViewData["HotestBlogs"] = articleService.GetArticlesByViewCountDesc(5);
			return PartialView(articleService.GetArticlesByViewCountDesc(5));
		}
	}
}
