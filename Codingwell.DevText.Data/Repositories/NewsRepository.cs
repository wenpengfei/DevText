using System;
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
	public interface INewsRepository : IRepository<News> {
		/// <summary>
		/// 获取某人的新闻
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		IEnumerable<News> GetNewsOfUser ( int userid );
		/// <summary>
		/// 获取某人的新闻
		/// </summary>
		/// <param name="userid"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		PagedList<News> GetNewsOfUser ( int userid, int pagesize, int pageindex );
		/// <summary>
		/// 获取文章
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		PagedList<News> GetNews ( int pagesize, int pageindex );
		/// <summary>
		/// 更新阅读数
		/// </summary>
		void UpdateViewCount ( int id );

		/// <summary>
		/// 获取某主题下的新闻
		/// </summary>
		/// <param name="topicid"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		PagedList<News> GetNewsByTopic ( int topicid, int pagesize, int pageindex );

		/// <summary>
		/// 获取某分类下的新闻
		/// </summary>
		/// <param name="categoryid"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		PagedList<News> GetNewsByCategory ( int categoryid, int pagesize, int pageindex );

		/// <summary>
		/// 某主题下的新闻数
		/// </summary>
		/// <param name="topicid"></param>
		/// <returns></returns>
		int GetNewsCountOfTopic ( int topicid );

		/// <summary>
		/// 某分类下的新闻数
		/// </summary>
		/// <param name="categoryid"></param>
		/// <returns></returns>
		int GetNewsCountOfCategory ( int categoryid );

		IEnumerable<News> GetNewsByViewCountDesc ( int count );
		IEnumerable<News> GetNewsByUpCountDesc ( int count );

	}
	#endregion

	public class NewsRepository : RepositoryBase<News>, INewsRepository {

		public NewsRepository ( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {
		}

		/// <summary>
		/// 获取某人的新闻
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		public IEnumerable<News> GetNewsOfUser ( int userid ) {
			return this.DB.News.Where(a => a.CreatorID == userid);
		}

		/// <summary>
		/// 获取某人的新闻
		/// </summary>
		/// <param name="userid"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public PagedList<News> GetNewsOfUser ( int userid, int pagesize, int pageindex ) {
			if (pageindex < 1)
				pageindex = 1;
			return this.DB.News.Where(a => a.CreatorID == userid).OrderByDescending(a => a.CreateTime).ToPagedList(pageindex, pagesize);
		}

		/// <summary>
		/// 分页
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public PagedList<News> GetNews ( int pagesize, int pageindex ) {
			return this.DB.News.OrderByDescending(a => a.CreateTime).ToPagedList(pageindex, pagesize);
		}

		public int GetNewsCountOfTopic ( int topicid ) {
			return this.DB.News.Where(a => a.TopicID == topicid).Count();
		}
		/// <summary>
		/// 获取主题下的文章
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="topicid"></param>
		/// <returns></returns>
		public PagedList<News> GetNewsByTopic ( int topicid, int pagesize, int pageindex ) {
			return this.DB.News.Where(a => a.TopicID == topicid)
				.OrderByDescending(a => a.CreateTime).ToPagedList(pageindex, pagesize);
		}


		public int GetNewsCountOfCategory ( int categoryid ) {
			return this.DB.News.Where(a => a.NewsCategories.Select(c => c.ID).Contains(categoryid)).Count();
		}
		/// <summary>
		/// 获取分类下的文章
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="categoryid"></param>
		/// <returns></returns>
		public PagedList<News> GetNewsByCategory ( int categoryid, int pagesize, int pageindex ) {
			return this.DB.News.Where(a => a.NewsCategories.Select(c => c.ID).Contains(categoryid))
				.OrderByDescending(a => a.CreateTime).ToPagedList(pageindex, pagesize);
		}

		public void UpdateViewCount ( int id ) {
			News temp = this.DB.News.FirstOrDefault(a => a.ID == id);
			if (temp != null)
				temp.PageViewCount += 1;
		}

		public IEnumerable<News> GetNewsByViewCountDesc ( int count ) {
			System.DateTime temp = System.DateTime.Now.AddDays(-14);
			return this.DB.News.Where(a => a.CreateTime >= temp).OrderByDescending(a => ( a.PageViewCount + a.RssViewCount )).Take(count);
		}

		public IEnumerable<News> GetNewsByUpCountDesc ( int count ) {
			System.DateTime temp = System.DateTime.Now.AddDays(-14);
			return this.DB.News.Where(a => a.CreateTime >= temp).OrderByDescending(a => a.UpCount).Take(count).OrderByDescending(a => a.CreateTime);
		}
	}
}
