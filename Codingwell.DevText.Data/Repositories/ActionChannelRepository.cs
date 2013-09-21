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

	public interface IActionChannelRepository : IRepository<ActionChannel> {
		PagedList<ActionChannel> GetActionChannels ( int pagesize, int pageindex, string keyword );
	}

	public class ActionChannelRepository : RepositoryBase<ActionChannel>, IActionChannelRepository {

		public ActionChannelRepository ( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {

		}
		/// <summary>
		/// 获取动作频道
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public PagedList<ActionChannel> GetActionChannels ( int pagesize, int pageindex, string keyword ) {
			if (string.IsNullOrEmpty(keyword)) {
				return this.DB.ActionChannels.Where(c => c.State == (int)RecordState.Approved).OrderByDescending(c => c.Rank).ToPagedList(pageindex, pagesize);
			} else {
				return this.DB.ActionChannels.Where(c => c.State == (int)RecordState.Approved && c.Name.Contains(keyword))
					.OrderByDescending(c => c.Rank).ToPagedList(pageindex, pagesize);
			}
		}
	}

}
