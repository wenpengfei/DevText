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

	public interface IActionRepository : IRepository<Action> {

		PagedList<Action> GetActions ( int pagesize, int pageindex, string keyword );

		IEnumerable<Action> GetActionsOfRole ( Role role );
	}

	public class ActionRepository : RepositoryBase<Action>, IActionRepository {

		public ActionRepository ( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {

		}

		/// <summary>
		/// 获取全部Action
		/// </summary>
		/// <returns></returns>
		public PagedList<Action> GetActions ( int pagesize, int pageindex, string keyword ) {
			if (string.IsNullOrEmpty(keyword)) {
				return this.DB.Actions.Where(c => c.State == (int)RecordState.Approved).OrderBy(c => c.ActionName).ToPagedList(pageindex, pagesize);
			} else {
				return this.DB.Actions.Where(c => c.State == (int)RecordState.Approved && ( c.ActionName.Contains(keyword) || c.ActionValue.Contains(keyword) ))
					.OrderBy(c => c.ActionName).ToPagedList(pageindex, pagesize);
			}
		}

		/// <summary>
		/// 获取角色的动作
		/// </summary>
		/// <param name="role"></param>
		/// <returns></returns>
		public IEnumerable<Action> GetActionsOfRole ( Role role ) {
			return this.DB.Actions.Where(a => a.Roles.Contains(role));
		}
	}
}
