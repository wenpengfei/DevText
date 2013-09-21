using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Data.Infrastructure;

namespace Codingwell.DevText.Data {

	public interface INewsCategoryRepository : IRepository<NewsCategory> {
		NewsCategory GetCategory ( string name );
	}

	public class NewsCategoryRepository : RepositoryBase<NewsCategory>, INewsCategoryRepository {

		public NewsCategoryRepository ( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {
		}

		public NewsCategory GetCategory ( string name ) {
			return this.DB.NewsCategories.FirstOrDefault(c => c.Name == name);
		}
	}
}
