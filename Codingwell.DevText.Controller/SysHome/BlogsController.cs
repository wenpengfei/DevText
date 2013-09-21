using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Service;
using Jayrock.Json.Conversion;
using QConnectSDK;
using QConnectSDK.Models;
using log4net;
using Webdiyer.WebControls.Mvc;
using AMicroblogAPI;
using AMicroblogAPI.Common;
using AMicroblogAPI.DataContract;
using Codingwell.Utility;

namespace Codingwell.DevText.Controller.SysHome {
	public class BlogsController : AuthBaseController {
		private readonly IArticleService articleService;
		private readonly IHomeContentService homecontentService;

		public BlogsController ( IArticleService articleservice, IHomeContentService homecontentService ) {
			this.articleService = articleservice;
			this.homecontentService = homecontentService;
		}

		//private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		[HttpGet]
		public ActionResult Index ( int? page ) {
			try {
				int _page = page ?? 1;
				return View(articleService.GetArticles(10, _page));
			} catch (Exception ex) {
				//log.Error(ex);
				return View();
			}
		}

		[HttpGet]
		public ActionResult My ( int? page ) {
			try {
				int _page = page ?? 1;
				return View(articleService.GetArticlesOfUser(UserContext.Instance.UserID, 10, _page));
			} catch (Exception ex) {
				//log.Error(ex);
				return View();
			}
		}

		[HttpGet]
		public ActionResult Post ( ) {
			try {
				return View(articleService.GetArticleCategoriesOfUser(UserContext.Instance.UserID));
			} catch (Exception ex) {
				//log.Error(ex);
				return View();
			}
		}

		[HttpPost]
		[ValidateInput(false)]
		public JsonResult SubmitPost ( string json ) {
			try {
				Article article = JsonConvert.Import<Article>(json);
				if (string.IsNullOrEmpty(article.WebFrom)) {
					article.WebFrom = UserContext.Instance.User.NickName;
					article.WebFromAddress = "/user/" + UserContext.Instance.UserID + "/";
				}
				var cates = article.ArticleCategories.ToList();
				article.ArticleCategories.Clear();
				foreach (var cate in cates) {
					var temp = articleService.GetArticleCategory(cate.ID);
					if (temp != null)
						article.ArticleCategories.Add(temp);
				}
				article.State = (int)RecordState.Approved;
				article.CreateTime = DateTime.Now;
				article.CreatorID = UserContext.Instance.UserID;
				article.AnymousComment = false;
				article.EnableComment = true;
				article.LastChanged = DateTime.Now;
				int aritlceid = articleService.CreateArticle(article);
				bool result = aritlceid > 0;
				if (result) {
					HomeContent content = new HomeContent();
					content.Title = article.Title;
					content.LinkUrl = string.Format("/blogs/detail/{0}", aritlceid);
					content.ContentType = (int)HomeContentType.Article;
					content.ContentData = article.Summary;
					content.InsertTime = DateTime.Now;
					homecontentService.CreateHomeContent(content);
					foreach (var auth in UserContext.Instance.User.OpenAuths) {
						switch ((OpenAuthType)auth.OpenAuthType) {
							case OpenAuthType.Sinaweibo: {
									try {
										UpdateStatusInfo updateStatusInfo = new UpdateStatusInfo();
										updateStatusInfo.Status = string.Format("【{0}】{1}@devtext http://www.devtext.com/blogs/detail/{2}",
																				article.Title.Length > 30
																					? article.Title.Substring(0, 30)
																					: article.Title,
																				article.Summary.Length > 90
																					? article.Summary.Substring(0, 90)
																					: article.Summary,
																				aritlceid);
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
											qzone.AddFeeds(article.Title.Length > 30 ? article.Title.Substring(0, 30) : article.Title,
														   string.Format("http://www.devtext.com/blogs/detail/{0}", aritlceid));
										}
									} catch (Exception ex) {

									}
									break;
								}
						}
					}
				}

				return Json(new { success = result, message = result ? "发布成功" : "发布失败" });
			} catch (Exception ex) {
				//log.Error(ex);
				return Json(new { success = false, message = "发生异常，请检查收再试" });
			}
		}
	}
}
