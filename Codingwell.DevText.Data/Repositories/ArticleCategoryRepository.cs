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
	public interface IArticleCategoryRepository : IRepository<ArticleCategory> {
		/// <summary>
		/// 获取用户的文章分类
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		IEnumerable<ArticleCategory> GetArticleCategoriesOfUser ( int userid );
		/// <summary>
		/// 验证用户的分类是否存在
		/// </summary>
		/// <param name="categoryname"></param>
		/// <param name="userid"></param>
		/// <returns></returns>
		bool ExistCategoryOfUser ( string categoryname, int userid );
	}

	public class ArticleCategoryRepository : RepositoryBase<ArticleCategory>, IArticleCategoryRepository {
		public ArticleCategoryRepository ( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {

		}

		/// <summary>
		/// 获取用户的文章分类
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		public IEnumerable<ArticleCategory> GetArticleCategoriesOfUser ( int userid ) {
			return this.DB.ArticleCategories.Where(c => c.OwnerID == userid);
		}
		/// <summary>
		/// 验证用户的分类是否存在
		/// </summary>
		/// <param name="categoryname"></param>
		/// <param name="userid"></param>
		/// <returns></returns>
		public bool ExistCategoryOfUser ( string categoryname, int userid ) {
			return this.DB.ArticleCategories.Where(c => c.OwnerID == userid && c.Name == categoryname).Count() > 0;
		}
	}
}
