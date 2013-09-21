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

	public interface IBookmarkService {
		int CreateBookmark ( Bookmark entity );
		bool UpdateBookmark ( Bookmark entity );
		Bookmark GetBookmark ( int id );
		PagedList<Bookmark> GetBookmarks ( int pagesize, int pageindex );
		int GetFavouritesCount ( );
		PagedList<Bookmark> GetBookmarksOfUser ( int pagesize, int pageindex, int userid );
		int GetBookmarksOfUserCount ( int userid );
	}

	public class BookmarkService :IBookmarkService{
		private readonly IBookmarkRepository bookmarkRepository;
		private readonly IUnitOfWork unitwork;
		//private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public BookmarkService (IUnitOfWork unitwork,IBookmarkRepository bookmarkRepository ) {
			this.unitwork = unitwork;
			this.bookmarkRepository = bookmarkRepository;
		}

		private void Save ( ) {
			unitwork.Save();
		}

		public int CreateBookmark ( Bookmark entity ) {
			try {
				bookmarkRepository.Add(entity);
				Save();
				return entity.ID;
			} catch (Exception ex){
				//log.Error(ex.Message, ex);
				return -1;
			}
		}

		public bool UpdateBookmark ( Bookmark entity ) {
			try {
				bookmarkRepository.Update(entity);
				Save();
				return true;
			} catch (Exception ex){
				//log.Error(ex.Message, ex);
				return false;
			}
		}

		public Bookmark GetBookmark ( int id ) {
			return bookmarkRepository.GetById(id);
		}

		public PagedList<Bookmark> GetBookmarks ( int pagesize, int pageindex ) {
			return bookmarkRepository.GetBookmarks(pagesize, pageindex);
		}

		public int GetFavouritesCount ( ) {
			return bookmarkRepository.GetBookmarksCount();
		}

		public PagedList<Bookmark> GetBookmarksOfUser ( int pagesize, int pageindex, int userid ) {
			return bookmarkRepository.GetBookmarksOfUser(userid, pagesize, pageindex);
		}

		public int GetBookmarksOfUserCount ( int userid ) {
			return bookmarkRepository.GetBookmarksCountOfUser(userid);
		}
	}
}
