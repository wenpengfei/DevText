using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Service;
using Jayrock.Json.Conversion;
using QConnectSDK;
using QConnectSDK.Models;
using log4net;
using AMicroblogAPI;
using AMicroblogAPI.Common;
using AMicroblogAPI.DataContract;

namespace Codingwell.DevText.Controller.SysHome {
	public class NewsController : AuthBaseController {
		//private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private readonly INewsService newsService;
		private readonly INewsTopicService newsTopicService;
		private readonly IHomeContentService homecontentService;

		public NewsController ( INewsService newsService, INewsTopicService newsTopicService, IHomeContentService homecontentService ) {
			this.newsService = newsService;
			this.newsTopicService = newsTopicService;
			this.homecontentService = homecontentService;
		}


		[HttpGet]
		public ActionResult Index ( int? page ) {
			try {
				int _page = page ?? 1;
				return View(newsService.GetNews(20, _page));
			} catch (Exception ex) {
				//log.Error(ex);
				return View();
			}

		}

		[HttpGet]
		public ActionResult PostByMe ( int? page ) {
			try {
				int _page = page ?? 1;
				return View(newsService.GetNewsOfUser(UserContext.Instance.UserID, 20, _page));
			} catch (Exception ex) {
				//log.Error(ex);
			}
			return View();
		}

		[HttpGet]
		public ActionResult PostNews ( ) {
			try {
				var cates = newsService.GetCategories();
				ViewData["Cates"] = new SelectList(from v in cates
												   select new { ID = v.ID, Text = v.Name }, "ID", "Text");
			} catch (Exception ex) {
				//log.Error(ex);
			}
			return View();
		}

		[HttpPost]
		[ValidateInput(false)]
		public JsonResult SubmitNews ( string json ) {
			try {
				News news = JsonConvert.Import<News>(json);
				var cates = news.NewsCategories.ToList();
				news.NewsCategories.Clear();
				foreach (var cate in cates) {
					var temp = newsService.GetCategory(cate.ID);
					if (temp != null)
						news.NewsCategories.Add(temp);
				}
				if (string.IsNullOrEmpty(news.WebFrom)) {
					news.WebFrom = UserContext.Instance.User.NickName;
					news.WebFromAddress = string.Format("/user/{0}/", UserContext.Instance.UserID);
				}
				//if (UserContext.Instance.User.((int)SysRoles.NewsEditor)) {
				news.State = (int)RecordState.Approved;
				//} else {
				//    news.CommentsCount = (int)RecordState.Pending;
				//}

				if (news.TopicID == 0) {
					news.TopicID = null;
				}
				news.CreateTime = DateTime.Now;
				news.CreatorID = UserContext.Instance.UserID;
				news.AnymousComment = false;
				news.EnableComment = true;
				news.LastChanged = DateTime.Now;

				int newsid = newsService.CreateNews(news);
				bool result = newsid > 0;
				if (result) {

					HomeContent content = new HomeContent();
					content.Title = news.Title;
					content.LinkUrl = string.Format("/news/detail/{0}", newsid);
					content.ContentType = (int)HomeContentType.News;
					content.ContentData = news.Summary;
					content.InsertTime = DateTime.Now;
					homecontentService.CreateHomeContent(content);
					foreach (var auth in UserContext.Instance.User.OpenAuths) {
						switch ((OpenAuthType)auth.OpenAuthType) {
							case OpenAuthType.Sinaweibo: {
									try {
										UpdateStatusInfo updateStatusInfo = new UpdateStatusInfo();
										updateStatusInfo.Status = string.Format("【{0}】{1}@devtext http://www.devtext.com/news/detail/{2}",
											news.Title.Length > 30 ? news.Title.Substring(0, 30) : news.Title,
											news.Summary.Length > 90 ? news.Summary.Substring(0, 90) : news.Summary,
																				newsid);
										AMicroblogAPI.AMicroblog.PostStatus(updateStatusInfo);
									} catch (Exception ex) {

									}
									break;
								}
							case OpenAuthType.QQ: {
									try {
										QConnectSDK.QOpenClient qzone =
											new QOpenClient(new OAuthToken() { AccessToken = auth.AccessTokenKey, OpenId = auth.OpenId });
										if (qzone != null) {
											qzone.AddFeeds(news.Title.Length > 30 ? news.Title.Substring(0, 30) : news.Title,
														   string.Format("http://www.devtext.com/news/detail/{0}", newsid));
										}
									} catch (Exception ex) {

									}
									break;
								}
						}
					}
				}

				return Json(new { success = result, message = result ? "投递成功" : "投递失败" });
			} catch (Exception ex) {
				//log.Error(ex);
				return Json(new { success = false, message = "发生异常，投递失败，请稍候再试" });
			}
		}

		[HttpPost]
		public JsonResult GetAllNewsTopics ( ) {
			var items = newsTopicService.GetAllNewsTopics();
			return Json(items.Select(b => new {
				ID = b.ID,
				FilePath = b.FilePath
			}), JsonRequestBehavior.AllowGet);
		}
	}
}
