using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Service;


namespace Codingwell.DevText.Controller {

	public class UserContext {
		
		public readonly static UserContext Instance = new UserContext();
		public const string SESSION_USER = "SESSION_USER";

		private UserContext ( ) {
		}

		public int UserID {
			get { return this.User.UserID; }
		}

		public User User {
			get {
				if (IsAuthenticated) {
					User user = null;
					if (HttpContext.Current.Session == null || HttpContext.Current.Session[SESSION_USER] == null) {
						user = DependencyResolver.Current.GetService<IUserService>().GetUser(HttpContext.Current.User.Identity.Name);
						HttpContext.Current.Session[SESSION_USER] = user;
					}
					user = (User)HttpContext.Current.Session[SESSION_USER];
					if (user != null) {
						if (user.UserID.ToString() != HttpContext.Current.User.Identity.Name) {
							user = DependencyResolver.Current.GetService<IUserService>().GetUser(HttpContext.Current.User.Identity.Name);
							HttpContext.Current.Session[SESSION_USER] = user;
						}
					}
					return user;
				} else
					return null;
			}
		}

		/// <summary>
		/// 是否已登录
		/// </summary>
		public bool IsAuthenticated {
			get {
				return HttpContext.Current.User.Identity.IsAuthenticated;
			}
		}

		/// <summary>
		/// 第三方登录
		/// </summary>
		/// <param name="openid"></param>
		/// <param name="clientIP"></param>
		/// <returns></returns>
		public AccountState OpenAuthLogin ( string openid, string clientIP ) {
			OpenAuth qauth = DependencyResolver.Current.GetService<IOpenAuthService>().GetOpenAuthEntityByOpenID(openid);
			if (qauth != null) {
				User entity = DependencyResolver.Current.GetService<IUserService>().GetUser(qauth.UserID);
				if (entity != null && entity.AccountState == (int)AccountState.Normal) {
					FormsAuthentication.SetAuthCookie(entity.UserID.ToString(), false);
					entity.LastLoginIP = clientIP;
					entity.LastLoginTime = DateTime.Now;
					DependencyResolver.Current.GetService<IUserService>().UpdateLastLoginInfo(entity);
					entity.OpenAuths.ToList();
					HttpContext.Current.Session[SESSION_USER] = entity;
					return (AccountState)entity.AccountState;
				} else {
					HttpContext.Current.Session.Remove(SESSION_USER);
					return entity == null ? AccountState.None : (AccountState)entity.AccountState;
				}
			} else {
				return AccountState.None;
			}
		}

		/// <summary>
		/// 登出
		/// </summary>
		public void Logout ( ) {
			FormsAuthentication.SignOut();
			HttpContext.Current.Session.Remove(SESSION_USER);
			HttpContext.Current.Session.RemoveAll();
			HttpContext.Current.Session.Abandon();
			HttpContext.Current.Request.Cookies.Clear();
		}

	}
}
