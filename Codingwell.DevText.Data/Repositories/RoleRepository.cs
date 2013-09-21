using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Data.Infrastructure;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Data {

	public interface IRoleRepository : IRepository<Role> {

		PagedList<Role> GetRoles ( int pagesize, int pageindex, string keyword );
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		IEnumerable<Role> GetUserRoles ( int userid );
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		IEnumerable<Action> GetUserActions ( int userid );
	}

	public class RoleRepository : RepositoryBase<Role>, IRoleRepository {

		public RoleRepository ( IDatabaseFactory databaseFactory ) :
			base(databaseFactory) {
		}

		public override void Add ( Role entity ) {
			if (this.DB.Roles.Where(r => r.Name == entity.Name).Count() > 0)
				return;
			this.DB.Roles.Add(entity);
		}

		public PagedList<Role> GetRoles ( int pagesize, int pageindex, string keyword ) {
			if (string.IsNullOrEmpty(keyword)) {
				return this.DB.Roles.Where(c => c.State == (int)RecordState.Approved).OrderByDescending(c => c.ID).ToPagedList(pageindex, pagesize);
			} else {
				return this.DB.Roles.Where(c => c.State == (int)RecordState.Approved && c.Name.Contains(keyword))
					.OrderByDescending(c => c.ID).ToPagedList(pageindex, pagesize);
			}
		}

		public IEnumerable<Role> GetUserRoles ( int userid ) {
			return this.DB.Roles.Where(r => r.Users.Select(u => u.UserID).Contains(userid));
		}

		public IEnumerable<Action> GetUserActions ( int userid ) {
			var roles = this.DB.Roles.Where(r => r.Users.Select(u => u.UserID).Contains(userid));
			IEnumerable<Action> actions = new List<Action>();
			foreach (var role in roles) {
				actions = actions.Union(role.Actions);
			}
			return actions;
		}
	}
}
