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

	public interface IArticleService {
		int CreateArticle ( Article entity );
		bool UpdateArticle ( Article entity );
		Article GetArticle ( int articleid );
		IEnumerable<Article> GetArticlesOfUser ( int userid );
		int GetArticlesCountOfUser ( int userid );
		PagedList<Article> GetArticlesOfUser ( int userid, int pagesize, int pageindex );
		PagedList<Article> GetArticles ( int pagesize, int pageindex );
		IEnumerable<Article> GetArticles ( );
		IEnumerable<Article> GetArticlesByTimeDesc ( int count );
		IEnumerable<Article> GetArticlesByViewCountDesc ( int count );
		IEnumerable<Article> GetArticlesByUpCountDesc ( int count );
		void UpdateViewCount ( int id );
		PagedList<Article> GetArticlesByCategory ( int pagesize, int pageindex, int categoryid );
		int GetArticlesCountOfCategory ( int categoryid );
		ArticleCategory GetArticleCategory ( int id );
		IEnumerable<ArticleCategory> GetArticleCategoriesOfUser ( int userid );
		bool ExistCategoryOfUser ( string categoryname, int userid );
	}

	public class ArticleService : IArticleService {
		private readonly IArticleRepository articleRepository;
		private readonly IArticleCategoryRepository articleCategoryRepository;
		private readonly IUnitOfWork unitwork;
		//private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public ArticleService ( IUnitOfWork unitwork, IArticleRepository articleRepository, IArticleCategoryRepository articleCategoryRepository ) {
			this.unitwork = unitwork;
			this.articleRepository = articleRepository;
			this.articleCategoryRepository = articleCategoryRepository;
		}
		private void Save ( ) {
			unitwork.Save();
		}

		/// <summary>
		/// 创建文章
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public int CreateArticle ( Article entity ) {
			try {
				articleRepository.Add(entity);
				Save();
				return entity.ID;
			}catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return -1;
			}
		}
		/// <summary>
		/// 更新文章
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool UpdateArticle ( Article entity ) {
			try {
				articleRepository.Update(entity);
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
		public Article GetArticle ( int articleid ) {
			return articleRepository.GetById(articleid);
		}
		/// <summary>
		/// 获取某人的新闻
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		public IEnumerable<Article> GetArticlesOfUser ( int userid ) {
			return articleRepository.GetArticlesOfUser(userid);
		}

		/// <summary>
		/// 获取某人文章的数量
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		public int GetArticlesCountOfUser ( int userid ) {
			return articleRepository.GetArticlesOfUser(userid).Count();
		}

		/// <summary>
		/// 获取某人的新闻
		/// </summary>
		/// <param name="userid"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public PagedList<Article> GetArticlesOfUser ( int userid, int pagesize, int pageindex ) {
			return articleRepository.GetArticlesOfUser(userid, pagesize, pageindex);
		}
		/// <summary>
		/// 获取文章
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public PagedList<Article> GetArticles ( int pagesize, int pageindex ) {
			return articleRepository.GetArticles(pagesize, pageindex);
		}
		/// <summary>
		/// 获取全部文章
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Article> GetArticles ( ) {
			return articleRepository.GetAll();
		}

		/// <summary>
		/// 按时间获取最新的几条
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public IEnumerable<Article> GetArticlesByTimeDesc ( int count ) {
			return articleRepository.GetArticles(count, 1);
		}
		/// <summary>
		/// 获取阅读最多的文章
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public IEnumerable<Article> GetArticlesByViewCountDesc ( int count ) {
			return articleRepository.GetArticlesByViewCountDesc(count);
		}

		/// <summary>
		/// 获取顶的最多的
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public IEnumerable<Article> GetArticlesByUpCountDesc ( int count ) {
			return articleRepository.GetArticlesByUpCountDesc(count);
		}

		public void UpdateViewCount ( int id ) {
			articleRepository.UpdateViewCount(id);
			Save();
		}
		/// <summary>
		/// 获取分类下的文章
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="categoryid"></param>
		/// <returns></returns>
		public PagedList<Article> GetArticlesByCategory ( int pagesize, int pageindex, int categoryid ) {
			return articleRepository.GetArticlesByCategory(categoryid, pagesize, pageindex);
		}

		public int GetArticlesCountOfCategory ( int categoryid ) {
			return articleRepository.GetArticlesCountOfCategory(categoryid);
		}

		public ArticleCategory GetArticleCategory ( int id ) {
			return articleCategoryRepository.GetById(id);
		}
		/// <summary>
		///
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		public IEnumerable<ArticleCategory> GetArticleCategoriesOfUser ( int userid ) {
			return articleCategoryRepository.GetArticleCategoriesOfUser(userid);
		}
		/// <summary>
		/// 是否已经存在此分类
		/// </summary>
		/// <param name="categoryname"></param>
		/// <param name="userid"></param>
		/// <returns></returns>
		public bool ExistCategoryOfUser ( string categoryname, int userid ) {
			return articleCategoryRepository.ExistCategoryOfUser(categoryname, userid);
		}
	}
}
