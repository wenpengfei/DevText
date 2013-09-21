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

	public interface IHomeContentService {
		PagedList<HomeContent> GetHomeContentByType ( int type, int pagesize );

		PagedList<HomeContent> GetHomeContentByType ( int type, int pageindex, int pagesize );

		int CreateHomeContent ( HomeContent content );

		bool UpdateHomeContent ( HomeContent content );
	}

	public class HomeContentService : IHomeContentService {
		private readonly IUnitOfWork unitwork;
		private readonly IHomeContentRepository homeRepository;

		public HomeContentService ( IUnitOfWork unitwork, IHomeContentRepository homeRepository ) {
			this.unitwork = unitwork;
			this.homeRepository = homeRepository;
		}

		private void Save ( ) {
			this.unitwork.Save();
		}

		public int CreateHomeContent ( HomeContent content ) {
			try {
				homeRepository.Add(content);
				Save();
				return content.ID;
			} catch {
				return -1;
			}
		}

		public bool UpdateHomeContent ( HomeContent content ) {
			try {
				homeRepository.Update(content);
				Save();
				return true;
			} catch {
				return false;
			}
		}

		public PagedList<HomeContent> GetHomeContentByType ( int type, int pagesize ) {
			return GetHomeContentByType(type, 1, pagesize);
		}

		public PagedList<HomeContent> GetHomeContentByType ( int type,int pageindex,int pagesize ) {
			return homeRepository.GetHomeContentsByType(type, pageindex, pagesize);
		}

	}
}
