using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Data.Infrastructure;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Data {
	#region Interface
	public interface IArticleRepository : IRepository<Article> {
		/// <summary>
		/// 获取用户的文章
		/// </summary>
		/// <param name="userid">用户ID</param>
		/// <returns></returns>
		IEnumerable<Article> GetArticlesOfUser ( int userid );
		/// <summary>
		/// 获取用户文章的分页数据
		/// </summary>
		/// <param name="userid">用户ID</param>
		/// <param name="pagesize">页面大小</param>
		/// <param name="pageindex">页码</param>
		/// <returns></returns>
		PagedList<Article> GetArticlesOfUser ( int userid, int pagesize, int pageindex );
		/// <summary>
		/// 获取全部文章
		/// </summary>
		/// <param name="pagesize">页面大小</param>
		/// <param name="pageindex">页码</param>
		/// <returns></returns>
		PagedList<Article> GetArticles ( int pagesize, int pageindex );
		/// <summary>
		/// 获取分类下的文章数目
		/// </summary>
		/// <param name="categoryid">文章分类ID</param>
		/// <returns></returns>
		int GetArticlesCountOfCategory ( int categoryid );
		/// <summary>
		/// 获取分类下的文章
		/// </summary>
		/// <param name="categoryid">文章分类ID</param>
		/// <param name="pagesize">页面大小</param>
		/// <param name="pageindex">页码</param>
		/// <returns></returns>
		PagedList<Article> GetArticlesByCategory ( int categoryid, int pagesize, int pageindex );
		/// <summary>
		/// 更新阅读数
		/// </summary>
		/// <param name="id"></param>
		void UpdateViewCount ( int id );

		IEnumerable<Article> GetArticlesByViewCountDesc ( int count );
		IEnumerable<Article> GetArticlesByUpCountDesc ( int count );
	}
	#endregion

	public class ArticleRepository : RepositoryBase<Article>, IArticleRepository {

		public ArticleRepository ( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {

		}

		/// <summary>
		/// 获取某人的新闻
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		public IEnumerable<Article> GetArticlesOfUser ( int userid ) {
			return this.DB.Articles.Where(a => a.CreatorID == userid);
		}

		/// <summary>
		/// 获取某人的新闻
		/// </summary>
		/// <param name="userid"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public PagedList<Article> GetArticlesOfUser ( int userid, int pagesize, int pageindex ) {
			return this.DB.Articles.Where(a => a.CreatorID == userid)
				.OrderByDescending(a => a.CreateTime).ToPagedList(pageindex, pagesize);
		}

		/// <summary>
		/// 分页
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public PagedList<Article> GetArticles ( int pagesize, int pageindex ) {
			return this.DB.Articles.OrderByDescending(a => a.CreateTime).ToPagedList(pageindex, pagesize);
		}

		public int GetArticlesCountOfCategory ( int categoryid ) {
			return this.DB.Articles.Where(a => a.ArticleCategories.Select(c => c.ID).Contains(categoryid)).Count();
		}
		/// <summary>
		/// 获取分类下的文章
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="categoryid"></param>
		/// <returns></returns>
		public PagedList<Article> GetArticlesByCategory ( int categoryid, int pagesize, int pageindex ) {
			return this.DB.Articles.Where(a => a.ArticleCategories.Select(c => c.ID).Contains(categoryid))
				.OrderByDescending(a => a.CreateTime).ToPagedList(pageindex, pagesize);
		}

		/// <summary>
		/// 更新阅读数
		/// </summary>
		/// <param name="id"></param>
		public void UpdateViewCount ( int id ) {
			Article temp = this.DB.Articles.FirstOrDefault(a => a.ID == id);
			if (temp != null)
				temp.PageViewCount += 1;
		}

		public IEnumerable<Article> GetArticlesByViewCountDesc ( int count ) {
			System.DateTime temp = System.DateTime.Now.AddDays(-14);
			return this.DB.Articles.Where(a => a.CreateTime >= temp).OrderByDescending(a => ( a.PageViewCount + a.RssViewCount )).Take(count);
		}

		public IEnumerable<Article> GetArticlesByUpCountDesc ( int count ) {
			System.DateTime temp = System.DateTime.Now.AddDays(-14);
			return this.DB.Articles.Where(a => a.CreateTime >= temp).OrderByDescending(a => a.UpCount).Take(count).OrderByDescending(a => a.CreateTime);
		}
	}
}
