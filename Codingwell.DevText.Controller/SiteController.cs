using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security;
using System.Security.Authentication;
using System.Configuration;
using System.Threading;

using Codingwell.DevText.Model;
using Codingwell.DevText.Model.ViewModel;
using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Service;
using Codingwell.Utility;
using Jayrock.Json.Conversion;
using AMicroblogAPI;
using AMicroblogAPI.Common;
using AMicroblogAPI.DataContract;
using QConnectSDK;
using QConnectSDK.Models;
using QConnectSDK.Context;
using MvcSiteMapProvider.Web;

namespace Codingwell.DevText.Controller {
	public class SiteController : BaseController {

		private readonly IUserService userService;
		private readonly IOpenAuthService openAuthService;
		private readonly IBuildLogService bulidLogService;
		private readonly IHomeContentService homecontentService;

		public SiteController ( IUserService userService, IOpenAuthService openAuthService, IBuildLogService bulidLogService, IHomeContentService homecontentservice ) {
			this.userService = userService;
			this.openAuthService = openAuthService;
			this.bulidLogService = bulidLogService;
			this.homecontentService = homecontentservice;
		}

		[HttpGet]
		public ActionResult Index ( ) {
			HomeViewModel model = new HomeViewModel();
			model.FoucsContent = homecontentService.GetHomeContentByType((int)HomeContentType.FoucsEvent, 5);
			model.Articles = homecontentService.GetHomeContentByType((int)HomeContentType.Article, 9);
			model.News = homecontentService.GetHomeContentByType((int)HomeContentType.News, 9);
			model.Links = homecontentService.GetHomeContentByType((int)HomeContentType.FriendLink, 10);
			return View(model);
		}

		[HttpGet]
		public ActionResult Login ( ) {
			return View();
		}

		#region BindUser
		//[HttpPost]
		//public JsonResult BindUser ( string username, string password ) {
		//    //if (Session["qzoneauthcallback"] == null)
		//    //	return Json(new { result = false, message = "QQ登录的会话已过期，请重新登录" });

		//    bool result = false;
		//    string message = string.Empty;
		//    username = username.Replace("'", "").Trim().ToLower();
		//    password = password.Replace("'", "");

		//    //AccountState loginStatus = UserContext.Instance.CheckLogin(username, AES.Encode(password), Utils.GetRealIP());
		//    AccountState loginStatus = AccountState.None;
		//    switch (loginStatus) {
		//        case AccountState.Normal: {
		//            User user = userService.GetUser(username);
		//                OpenAuth qqauth = openAuthService.GetOpenAuthEntityByUserID(user.UserID);
		//                if (qqauth != null) {
		//                    message = "该账户已绑定了其他账户";
		//                    result = false;
		//                } else {
		//                    QzoneSDK.Qzone qzone = Session["qzoneauthcallback"] as QzoneSDK.Qzone;
		//                    var currentUser = qzone.GetCurrentUser();
		//                    var qzoneuser = (BasicProfile)JsonConvert.Import(typeof(BasicProfile), currentUser);
		//                    user.NickName = qzoneuser.Nickname;
		//                    user.Figureurl = qzoneuser.Figureurl;
		//                    userService.UpdateUser(user);
		//                    qqauth = new OpenAuth();
		//                    qqauth.UserID = user.UserID;
		//                    qqauth.OpenId = qzone.OpenID;
		//                    qqauth.AccessTokenKey = qzone.OAuthTokenKey;
		//                    qqauth.AccessTokenSecret = qzone.OAuthTokenSecret;
		//                    qqauth.InsertTime = DateTime.Now;
		//                    openAuthService.CreateOpenAuth(qqauth);
		//                    message = "绑定成功";
		//                    result = true;
		//                }
		//                break;
		//            }
		//        case AccountState.Forbidden: {
		//                message = "账户被锁定，禁止登录";
		//                result = false;
		//                break;
		//            }
		//        default: {
		//                message = "用户名或密码错误！";
		//                result = false;
		//                break;
		//            }
		//    }
		//    return Json(new { result = result, message = message });
		//}
		#endregion

		[HttpGet]
		public ActionResult Logout ( ) {
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetNoStore();
			System.Web.HttpContext.Current.Session.RemoveAll();
			UserContext.Instance.Logout();
			return new RedirectResult("/");
		}


		public ActionResult QzoneLogin ( ) {
			var context = new QzoneContext();
			string state = Guid.NewGuid().ToString().Replace("-", "");
			string scope = "get_user_info,add_share,list_album,upload_pic,check_page_fans,add_t,add_pic_t,del_t,get_repost_list,get_info,get_other_info,get_fanslist,get_idolist,add_idol,del_idol,add_one_blog,add_topic,get_tenpay_addr";
			var authenticationUrl = context.GetAuthorizationUrl(state, scope);
			HttpContext.Session["requeststate"] = state;
			return new RedirectResult(authenticationUrl);
		}

		public void QzoneCallback ( ) {
			if (Request.Params["code"] != null) {
				var verifier = Request.Params["code"];
				QOpenClient qzone = null;
				QConnectSDK.Models.User currentUser = null;
				string state = Session["requeststate"].ToString();
				qzone = new QOpenClient(verifier, state);
				currentUser = qzone.GetCurrentUser();
				Session["QzoneOauth"] = qzone;
				if (null != currentUser) {
					OpenAuth openauth = openAuthService.GetOpenAuthEntityByOpenID(qzone.OAuthToken.OpenId);
					if (openauth != null) {
						if (openauth.AccessTokenKey != qzone.OAuthToken.AccessToken || openauth.AccessTokenSecret != qzone.OAuthToken.AccessToken) {
							openauth.AccessTokenKey = qzone.OAuthToken.AccessToken;
							openauth.AccessTokenSecret = qzone.OAuthToken.AccessToken;
							openAuthService.UpdateOpenAuth(openauth);
						}
						AccountState accountstate = UserContext.Instance.OpenAuthLogin(qzone.OAuthToken.OpenId, Utils.GetRealIP());
						switch (accountstate) {
							case AccountState.Normal: {
									Response.Write("<script>opener.location.href=opener.location.href;window.close();</script>");
									break;
								}
							//case AccountState.PendingActived: {
							//        Model.Entities.User userentity = userService.GetUser(openauth.UserID);
							//        if (string.IsNullOrEmpty(userentity.Email)) {
							//            Response.Write("<script>alert(\"尚未设置邮箱!\");opener.location.href=\"/site/updateemail\";window.close();</script>");
							//        } else {
							//            Response.Write("<script>alert(\"账户尚未激活!\");opener.location.href=\"/site/activeuser\";window.close();</script>");
							//        }
							//        break;
							//    }
							default: {
									Response.Write("<script>opener.location.href =\"/site/login\";window.close();</script>");
									break;
								}
						}
					} else {
						Model.Entities.User userEntity = new Model.Entities.User();
						userEntity.NickName = currentUser.Nickname;
						userEntity.Figureurl = currentUser.Figureurl;
						userEntity.Figureurl50 = currentUser.Figureurl;
						userEntity.Figureurl100 = currentUser.Figureurl;
						userEntity.CreateTime = DateTime.Now;
						userEntity.LastLoginTime = DateTime.Now;
						userEntity.OpenAuthType = (int)OpenAuthType.QQ;
						userEntity.AccountState = (int)AccountState.Normal;
						userEntity.ActiveCode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Guid.NewGuid().ToString(), "md5");
						int userid = userService.CreateUser(userEntity);
						HttpContext.Session["UserId"] = userid;
						openauth = new OpenAuth();
						openauth.UserID = userid;
						openauth.OpenId = qzone.OAuthToken.OpenId;
						openauth.AccessTokenKey = qzone.OAuthToken.AccessToken;
						openauth.AccessTokenSecret = qzone.OAuthToken.AccessToken;
						openauth.InsertTime = DateTime.Now;
						openAuthService.CreateOpenAuth(openauth);
						UserContext.Instance.OpenAuthLogin(qzone.OAuthToken.OpenId, Utils.GetRealIP());
						Response.Write("<script>opener.location.href=opener.location.href;window.close();</script>");
					}
				}
			}
		}

		public ActionResult SinaLogin ( ) {
			var requestToken = AMicroblog.GetRequestToken();
			HttpContext.Session["RequestToken"] = requestToken;
			var callback = ConfigurationManager.AppSettings["callbackUrl"];
			var callbackUrl = HttpUtility.UrlEncode(callback);
			return new RedirectResult(( string.Format("{0}?oauth_token={1}&oauth_callback={2}", APIUri.Authorize, requestToken.Token, callbackUrl) ));
		}

		public void SinaCallback ( ) {
			var verifer = Request.QueryString["oauth_verifier"];
			if (verifer != null) {
				var reqToken = HttpContext.Session["RequestToken"] as OAuthRequestToken;
				var accessToken = AMicroblogAPI.AMicroblog.GetAccessToken(reqToken, verifer);
				HttpContext.Session["AccessToken"] = accessToken;
				AMicroblogAPI.Environment.AccessToken = accessToken;
				OpenAuth auth = openAuthService.GetOpenAuthEntityByOpenID(accessToken.UserID);
				if (auth != null) {
					if (auth.AccessTokenKey != accessToken.Token || auth.AccessTokenSecret != accessToken.Secret) {
						auth.AccessTokenKey = accessToken.Token;
						auth.AccessTokenSecret = accessToken.Secret;
						openAuthService.UpdateOpenAuth(auth);
					}
					AccountState state = UserContext.Instance.OpenAuthLogin(accessToken.UserID, Utils.GetRealIP());
					switch (state) {
						case AccountState.Normal: {
								Response.Write("<script>opener.location.href=\"/\";window.close();</script>");
								break;
							}
						//case AccountState.PendingActived: {
						//        Model.Entities.User user = userService.GetUser(auth.UserID);
						//        if (string.IsNullOrEmpty(user.Email)) {
						//            Response.Write("<script>alert(\"尚未设置邮箱!\");opener.location.href=\"/site/updateemail\";window.close();</script>");
						//        } else {
						//            Response.Write("<script>alert(\"账户尚未激活!\");opener.location.href=\"/site/activeuser\";window.close();</script>");
						//        }
						//        break;
						//    }
						default: {
								Response.Write("<script>opener.location.href =\"/site/login\";window.close();</script>");
								break;
							}
					}
				} else {
					AMicroblogAPI.DataContract.UserInfo userInfo = AMicroblogAPI.AMicroblog.GetUserInfo(Convert.ToInt64(accessToken.UserID));
					Codingwell.DevText.Model.Entities.User user = new Codingwell.DevText.Model.Entities.User();
					user.NickName = userInfo.ScreenName;
					user.Figureurl50 = userInfo.ProfileImageUrl;
					user.LastLoginTime = DateTime.Now;
					user.CreateTime = DateTime.Now;
					user.OpenAuthType = (int)OpenAuthType.Sinaweibo;
					user.AccountState = (int)AccountState.Normal;
					user.ActiveCode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Guid.NewGuid().ToString(), "md5");
					int userid = userService.CreateUser(user);
					HttpContext.Session["UserId"] = userid;
					auth = new OpenAuth();
					auth.OpenId = accessToken.UserID;
					auth.AccessTokenKey = accessToken.Token;
					auth.AccessTokenSecret = accessToken.Secret;
					auth.UserID = userid;
					auth.InsertTime = DateTime.Now;
					openAuthService.CreateOpenAuth(auth);
					UserContext.Instance.OpenAuthLogin(accessToken.UserID, Utils.GetRealIP());
					Response.Write("<script>opener.location.href=opener.location.href;window.close();</script>");
				}
			}

		}

		[HttpGet]
		public ActionResult UpdateEmail ( ) {
			if (HttpContext.Session["UserId"] == null)
				return RedirectToAction("Index");
			else {
				return View();
			}
		}

		[HttpPost]
		public JsonResult UpdateEmail ( string email ) {
			if (HttpContext.Session["UserId"] == null)
				return Json(new { result = false, message = "会话超时" });

			Codingwell.DevText.Model.Entities.User user = userService.GetUserByEmail(email.ToLower());
			if (user != null) {
				return Json(new { result = false, message = "邮箱已经被注册" });
			}
			int userid = Convert.ToInt32(HttpContext.Session["UserId"]);
			user = userService.GetUser(userid);
			user.Email = email;
			userService.UpdateUser(user);

			//发送激活邮件
			( new Thread(new ParameterizedThreadStart(delegate( object o ) {
				userService.SendActiveEmail(o as Codingwell.DevText.Model.Entities.User);
			})) ).Start(user);
			return Json(new { result = true, message = "创建成功\r\n系统已发送了一份邮件给您的邮箱\r\n请按邮件中提供的地址和激活码激活您的账号" });
		}

		[HttpGet]
		public ActionResult Info ( ) {
			return View();
		}

		[HttpGet]
		public ActionResult ContactUs ( ) {
			return View();
		}

		[HttpGet]
		public ActionResult BuildLog ( ) {
			ViewData["BuildLog"] = bulidLogService.GetBuildLogs();
			return View();
		}

		[HttpGet]
		public ActionResult ActiveUser ( ) {
			return View();
		}

		[HttpPost]
		public JsonResult Activeuser ( string code ) {
			return Json(userService.ActiveUser(code));
		}

		[HttpGet]
		public ActionResult Error ( string error ) {
			ViewData["ErrorInfo"] = error;
			return View();
		}

		[HttpGet]
		public ActionResult SiteMap ( ) {
			return View();
		}

		[HttpGet]
		public ActionResult SiteMapXml ( ) {
			return new XmlSiteMapResult();
		}
	}



}
