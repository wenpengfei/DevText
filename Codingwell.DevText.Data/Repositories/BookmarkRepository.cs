using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

using Codingwell.DevText.Data.Infrastructure;
using Codingwell.DevText.Model.Entities;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Data {
	#region Interface
		 
	public interface IBookmarkRepository : IRepository<Bookmark> {
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		int GetBookmarksCount ( );
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		PagedList<Bookmark> GetBookmarks ( int pagesize, int pageindex );
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userid"></param>
		/// <returns></returns>
		int GetBookmarksCountOfUser ( int userid );
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userid"></param>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		PagedList<Bookmark> GetBookmarksOfUser ( int userid, int pagesize, int pageindex );

		
	}
	#endregion

	public class BookmarkRepository : RepositoryBase<Bookmark>, IBookmarkRepository {

		public BookmarkRepository ( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {

		}

		/// <summary>
		/// 获取收藏
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <returns></returns>
		public PagedList<Bookmark> GetBookmarks ( int pagesize, int pageindex ) {
			return this.DB.Bookmarks.OrderByDescending(f => f.InsertTime).ToPagedList(pageindex, pagesize);
		}

		public int GetBookmarksCount ( ) {
			return this.DB.Bookmarks.Count();
		}

		/// <summary>
		/// 获取某人的收藏
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="userid"></param>
		/// <returns></returns>
		public PagedList<Bookmark> GetBookmarksOfUser ( int userid, int pagesize, int pageindex ) {
			return this.DB.Bookmarks.Where(f => f.CreatorID == userid).OrderByDescending(f => f.InsertTime).ToPagedList(pageindex, pagesize);
		}

		public int GetBookmarksCountOfUser ( int userid ) {
			return this.DB.Bookmarks.Where(f => f.CreatorID == userid).Count();
		}
	}
}
