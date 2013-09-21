using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Data;
using Codingwell.DevText.Data.Infrastructure;
using log4net;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Service {

	public interface INewsService {
		int CreateNews ( News entity );
		bool UpdateNews ( News entity );
		News GetNews ( int articleid );
		IEnumerable<News> GetNewsOfUser ( int userid );
		PagedList<News> GetNewsOfUser ( int userid, int pagesize, int pageindex );
		PagedList<News> GetNews ( int pagesize, int pageindex );
		IEnumerable<News> GetNews ( );
		IEnumerable<News> GetNewsByTimeDesc ( int count );
		IEnumerable<News> GetNewsByViewCountDesc ( int count );
		IEnumerable<News> GetNewsByUpCountDesc ( int count );
		void UpdateViewCount ( int id );
		PagedList<News> GetNewsByTopic ( int pagesize, int pageindex, int topicid );
		int GetNewsCountOfTopic ( int topicid );
		PagedList<News> GetNewsByCategory ( int pagesize, int pageindex, int categoryid );
		int GetNewsCountOfCategory ( int categoryid );
		bool CreateCategory ( NewsCategory category );
		NewsCategory GetCategory ( int id );
		NewsCategory GetCategory ( string name );
		IEnumerable<NewsCategory> GetCategories ( );
	}

	public class NewsService : INewsService {
		private readonly INewsRepository newsRepository;
		private readonly INewsCategoryRepository newsCategoryRepository;
		private readonly IUnitOfWork unitwork;
		//private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public NewsService ( IUnitOfWork unitwork, INewsRepository newsRepository, INewsCategoryRepository newsCategoryRepository ) {
			this.unitwork = unitwork;
			this.newsRepository = newsRepository;
			this.newsCategoryRepository = newsCategoryRepository;
		}

		private void Save ( ) {
			unitwork.Save();
		}

		/// <summary>
		/// 创建文章
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public int CreateNews ( News entity ) {
			try {
				newsRepository.Add(entity);
				Save();
				return entity.ID;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return -1;
			}
		}
		/// <summary>
		/// 更新文章
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool UpdateNews ( News entity ) {
			try {
				newsRepository.Update(entity);
				Save();
				return true;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return false;
			}
		}

		/// <summary>
		/// 获取文章
		/// </summary>
		/// <param name="articleid"></param>
		/// <returns></returns>
		public News GetNews ( int articleid ) {
			return newsRepository.GetById(articleid);
		}
		/// <summary>
		/// 获取某人的新闻
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		public IEnumerable<News> GetNewsOfUser ( int userid ) {
			return newsRepository.GetNewsOfUser(userid);
		}
		/// <summary>
		/// 获取某人的新闻
		/// </summary>
		/// <param name="userid"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public PagedList<News> GetNewsOfUser ( int userid, int pagesize, int pageindex ) {
			return newsRepository.GetNewsOfUser(userid, pagesize, pageindex);
		}
		/// <summary>
		/// 获取文章
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public PagedList<News> GetNews ( int pagesize, int pageindex ) {
			return newsRepository.GetNews(pagesize, pageindex);
		}
		/// <summary>
		/// 获取全部文章
		/// </summary>
		/// <returns></returns>
		public IEnumerable<News> GetNews ( ) {
			return newsRepository.GetAll();
		}

		/// <summary>
		/// 按时间获取最新的几条
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public IEnumerable<News> GetNewsByTimeDesc ( int count ) {
			return newsRepository.GetNews(count, 1);
		}
		/// <summary>
		/// 48小时内获取阅读最多的文章
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public IEnumerable<News> GetNewsByViewCountDesc ( int count ) {
			return newsRepository.GetNewsByViewCountDesc(count);
		}

		/// <summary>
		/// 获取顶的最多的
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public IEnumerable<News> GetNewsByUpCountDesc ( int count ) {
			return newsRepository.GetNewsByUpCountDesc(count);
		}

		public void UpdateViewCount ( int id ) {
			newsRepository.UpdateViewCount(id);
			Save();
		}
		/// <summary>
		/// 获取主题下的文章
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="topicid"></param>
		/// <returns></returns>
		public PagedList<News> GetNewsByTopic ( int pagesize, int pageindex, int topicid ) {
			return newsRepository.GetNewsByTopic(topicid, pagesize, pageindex);
		}

		public int GetNewsCountOfTopic ( int topicid ) {
			return newsRepository.GetNewsCountOfTopic(topicid);
		}
		/// <summary>
		/// 获取分类下的文章
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="categoryid"></param>
		/// <returns></returns>
		public PagedList<News> GetNewsByCategory ( int pagesize, int pageindex, int categoryid ) {
			return newsRepository.GetNewsByCategory(categoryid, pagesize, pageindex);
		}

		public int GetNewsCountOfCategory ( int categoryid ) {
			return newsRepository.GetNewsCountOfCategory(categoryid);
		}

		#region Cateogry
		/// <summary>
		/// 创建分类
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		public bool CreateCategory ( NewsCategory category ) {
			try {
				newsCategoryRepository.Add(category);
				Save();
				return true;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return false;
			}
		}
		/// <summary>
		/// 按ID获取分类
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public NewsCategory GetCategory ( int id ) {
			return newsCategoryRepository.GetById(id);
		}
		/// <summary>
		/// 按名称获取分类
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public NewsCategory GetCategory ( string name ) {
			return newsCategoryRepository.GetCategory(name);
		}
		/// <summary>
		/// 获取全部新闻分类
		/// </summary>
		/// <returns></returns>
		public IEnumerable<NewsCategory> GetCategories ( ) {
			return newsCategoryRepository.GetAll();
		}
		#endregion
	}
}
