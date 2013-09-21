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
	public class NewsController : BaseController {

		private readonly INewsService newsService;
		private readonly INewsTopicService newsTopicService;

		public NewsController ( INewsService newsService, INewsTopicService newsTopicService ) {
			this.newsService = newsService;
			this.newsTopicService = newsTopicService;
		}


		// /news
		// /news/index
		[HttpGet]
		public ActionResult Index ( int? page ) {
			int _page = page ?? 1;
			var models = newsService.GetNews(20, _page);
			return View(models);
		}

		[HttpGet]
		public ActionResult Detail ( int id, string path ) {
			News article = newsService.GetNews(id);
			if (article == null) {
				throw new HttpException(404, "文章不存在");
			}
			if (( !string.IsNullOrEmpty(path) || !string.IsNullOrEmpty(article.Path) ) && !String.Equals(path, article.Path, StringComparison.OrdinalIgnoreCase)) {
				return this.RedirectToActionPermanent("detail", new { id = article.ID, path = article.Path });
			}
			newsService.UpdateViewCount(id);
			return View(article);
		}

		[HttpGet]
		public ActionResult Category ( int id, int? page ) {
			NewsCategory cate = newsService.GetCategory(id);
			if (cate == null)
				throw new HttpException(404, "文章分类不存在");
			int _page = page ?? 1;
			NewsOfCategory newsOfCategory = new NewsOfCategory();
			newsOfCategory.Category = cate;
			newsOfCategory.News = newsService.GetNewsByCategory(20, _page, id);
			return View(newsOfCategory);
		}

		[HttpGet]
		public ActionResult Topic ( int id, int? page ) {
			NewsTopic topic = newsTopicService.GetTopic(id);
			if (topic == null)
				throw new HttpException(404, "文章主题不存在");
			int _page = page ?? 1;
			NewsOfTopic newsOfTopic = new NewsOfTopic();
			newsOfTopic.Topic = topic;
			newsOfTopic.News = newsService.GetNewsByTopic(20, _page, id);
			return View(newsOfTopic);
		}

		[HttpGet]
		[ChildActionOnly]
		public PartialViewResult NewsSilde ( ) {
			return PartialView();
		}

	}
}
