using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Data.Infrastructure;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Data {

	public interface IHomeContentRepository : IRepository<HomeContent> {
		PagedList<HomeContent> GetHomeContentsByType ( int type, int pageindex, int pagesize );
	}

	public class HomeContentRepository : RepositoryBase<HomeContent>, IHomeContentRepository {

		public HomeContentRepository ( IDatabaseFactory databaseFactory ) : base(databaseFactory) { }

		public PagedList<HomeContent> GetHomeContentsByType ( int type ,int pageindex,int pagesize) {
			return this.DB.HomeContents.OrderByDescending(c => c.InsertTime).Where(c => c.ContentType == type).ToPagedList(pageindex, pagesize);
		}
	}
}
