using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Data;
using Codingwell.DevText.Data.Infrastructure;
using Codingwell.Utility;
using Codingwell.DevText.Utility;
using log4net;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Service {

	public interface IUserService {
		//AccountState CheckLogin ( string loginname, string password );
		User GetUser ( string p );
		User GetUser ( int userid );
		User GetUserByEmail ( string email );
		void UpdateLastLoginInfo ( User user );
		bool UpdateUser ( User user );
		PagedList<User> GetUsers ( int pagesize, int pageindex, string keyword );
		IEnumerable<User> GetUsers ( );
		int CreateUser ( User user );
		bool ActiveUser ( string code );
		void SendActiveEmail ( User user );
	}
	public class UserService : IUserService {
		private readonly IUserRepository userRepository;
		private readonly IUnitOfWork unitOfWork;
		//private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public UserService (IUnitOfWork unitwork,IUserRepository userRepository ) {
			this.unitOfWork = unitwork;
			this.userRepository = userRepository;
		}

		private void Save ( ) {
			unitOfWork.Save();
		}

		///// <summary>
		///// 验证用户登录
		///// </summary>
		///// <param name="loginname"></param>
		///// <param name="password"></param>
		///// <returns></returns>
		//public AccountState CheckLogin ( string loginname, string password ) {
		//    User entity = userRepository.CheckLogin(loginname.Replace("'", ""), loginname.Replace("'", ""), password);
		//    if (entity == null)
		//        return AccountState.None;
		//    else
		//        return (AccountState)entity.AccountState;
		//}
		/// <summary>
		/// 获取用户
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public User GetUser ( string p ) {
			return userRepository.GetUser(p);
		}
		/// <summary>
		/// 获取用户
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		public User GetUser ( int userid ) {
			return userRepository.GetUser(userid);
		}
		/// <summary>
		/// 根据邮箱获取用户
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public User GetUserByEmail ( string email ) {
			return userRepository.GetUserByEmail(email);
		}
		/// <summary>
		/// 更新登录信息
		/// </summary>
		/// <param name="user"></param>
		public void UpdateLastLoginInfo ( User user ) {
			userRepository.Update(user);
			Save();
		}

		public bool UpdateUser ( User user ) {
			try {
				userRepository.Update(user);
				Save();
				return true;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return false;
			}
		}
		/// <summary>
		/// 获取用户
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public PagedList<User> GetUsers ( int pagesize, int pageindex, string keyword ) {
			return userRepository.GetUsers(pagesize, pageindex, keyword);
		}
		/// <summary>
		/// 创建用户
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public int CreateUser ( User user ) {
			try {
				userRepository.Add(user);
				Save();
				return user.UserID;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return -1;
			}
		}

		/// <summary>
		/// 激活用户
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public bool ActiveUser ( string code ) {
			try {
				userRepository.ActiveUser(code);
				Save();
				return true;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return false;
			}
		}

		/// <summary>
		/// 发送激活邮件
		/// </summary>
		/// <param name="user"></param>
		public void SendActiveEmail ( User user ) {
			string subject = "Devtext 注册激活邮件";
			StringBuilder strBul = new StringBuilder();
			strBul.Append("<html><head><title>Devtext 注册激活邮件</title></head>");
			strBul.Append("<body>");
			strBul.Append("<b>亲爱的" + user.NickName + "： </b>");
			strBul.Append("<p>感谢您注册Devtext.com，为了防止机器人注册、让您更好更安全地使用我们的网站，需要您激活您的账号，确认您邮箱的可用性。</p>");
			strBul.Append("<p>激活码：" + user.ActiveCode + "</p>");
			strBul.Append("<p>激活路径：<a href=\"http://www.devtext.com/site/activeuser\">http://www.devtext.com/site/activeuser</a></p>");
			strBul.Append("<p>再次感谢您的支持，谢谢。</p>");
			strBul.Append("<p>备注：本邮件由系统自动发出，请勿直接回复邮件，谢谢。</p>");
			strBul.Append("</body>");
			strBul.Append("</html>");
			MailService.SendMail(subject, strBul.ToString(), user.Email, string.Empty, string.Empty);
		}

		public IEnumerable<User> GetUsers ( ) {
			return userRepository.GetAll();
		}
	}
}
