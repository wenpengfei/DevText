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

	public interface IEventlogRepository : IRepository<EventLog> {
		/// <summary>
		/// 获取动静
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		PagedList<EventLog> GetEventlogs ( int pagesize, int pageindex );
		/// <summary>
		/// 获取某人动静
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="userid"></param>
		/// <returns></returns>
		PagedList<EventLog> GetEventlogs ( int pagesize, int pageindex, int userid );
	}

	public class EventlogRepository : RepositoryBase<EventLog>, IEventlogRepository {

		public EventlogRepository ( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public PagedList<EventLog> GetEventlogs ( int pagesize, int pageindex ) {
			return this.DB.EventLogs.OrderByDescending(e => e.OccurTime).ToPagedList(pageindex, pagesize);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="userid"></param>
		/// <returns></returns>
		public PagedList<EventLog> GetEventlogs ( int pagesize, int pageindex, int userid ) {
			return this.DB.EventLogs.Where(e => e.UserID == userid).OrderByDescending(e => e.OccurTime).ToPagedList(pageindex, pagesize);
		}
	}
}
