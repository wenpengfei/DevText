using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Data.Infrastructure;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Data {
	public interface IUserRepository : IRepository<User> {
		/// <summary>
		/// 验证登录
		/// </summary>
		/// <param name="loginname"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		//User CheckLogin ( string loginname, string email, string password );

		User GetUser ( string userid );

		User GetUser ( int userid );

		User GetUserByEmail ( string email );

		void ActiveUser ( string code );

		PagedList<User> GetUsers ( int pagesize, int pageindex, string keyword );
	}

	public class UserRepository : RepositoryBase<User>, IUserRepository {

		public UserRepository ( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {
		}

		///// <summary>
		///// 验证登录
		///// </summary>
		///// <param name="loginname"></param>
		///// <param name="password"></param>
		///// <returns></returns>
		//public User CheckLogin ( string loginname, string email, string password ) {
		//    return this.DB.Users.FirstOrDefault(u => ( !string.IsNullOrEmpty(loginname) && u.LoginName == loginname && u.Password == password )
		//        || ( !string.IsNullOrEmpty(loginname) && u.Email == email && u.Password == password ));
		//}

		/// <summary>
		/// 获取用户
		/// </summary>
		/// <param name="loginname"></param>
		/// <returns></returns>
		public User GetUser ( string userid ) {
			return this.DB.Users.FirstOrDefault(u => u.UserID.ToString() == userid);
		}

		/// <summary>
		/// 获取用户
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		public User GetUser ( int userid ) {
			return this.DB.Users.FirstOrDefault(u => u.UserID == userid);
		}
		/// <summary>
		/// 通过email获取用户
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public User GetUserByEmail ( string email ) {
			return this.DB.Users.FirstOrDefault(u => u.Email.Contains(email));
		}

		public override void Add ( User entity ) {
			this.DB.Users.Add(entity);
		}

		/// <summary>
		/// 激活账户
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public void ActiveUser ( string code ) {
			if (string.IsNullOrEmpty(code))
				return;
			User temp = this.DB.Users.FirstOrDefault(u => u.ActiveCode == code && u.AccountState == (int)AccountState.PendingActived);
			if (temp == null)
				return;
			temp.AccountState = (int)AccountState.Normal;
		}

		public PagedList<User> GetUsers ( int pagesize, int pageindex, string keyword ) {
			if (string.IsNullOrEmpty(keyword)) {
				return this.DB.Users.OrderByDescending(a => a.CreateTime).ToPagedList(pageindex, pagesize);
			} else {
				return this.DB.Users.Where(a => a.NickName.Contains(keyword))
					.OrderByDescending(a => a.CreateTime).ToPagedList(pageindex, pagesize);
			}
		}
	}
}
