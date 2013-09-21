using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

using Codingwell.DevText.Data.Infrastructure;
using Codingwell.DevText.Model.Entities;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Data {
	public interface INewsTopicRepository : IRepository<NewsTopic> {
		PagedList<NewsTopic> GetNewsTopics ( int pagesize, int pageindex );
	}

	public class NewsTopicRepository : RepositoryBase<NewsTopic>, INewsTopicRepository {

		public NewsTopicRepository ( IDatabaseFactory databaseFactory ) :
			base(databaseFactory) {
		}

		/// <summary>
		/// 获取Topic
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="isTotal"></param>
		/// <returns></returns>
		public PagedList<NewsTopic> GetNewsTopics ( int pagesize, int pageindex ) {
			return this.DB.NewsTopics.OrderBy(t => t.SortKey).ToPagedList(pageindex, pagesize);
		}
	}
}
