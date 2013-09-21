using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Data;
using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Data.Infrastructure;
using log4net;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Service {

	public interface INewsTopicService {
		int CreateNewsTopic ( NewsTopic topic );
		bool UpdateNewsTopic ( NewsTopic topic );
		NewsTopic GetTopic ( int id );
		IEnumerable<NewsTopic> GetAllNewsTopics ( );
		PagedList<NewsTopic> GetNewsTopics ( int pagesize, int pageindex );
	}

	public class NewsTopicService : INewsTopicService {
		private readonly INewsTopicRepository newsTopicRepository;
		private readonly IUnitOfWork unitwork;
		//private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public NewsTopicService ( IUnitOfWork unitwork, INewsTopicRepository newstopicRepository ) {
			this.unitwork = unitwork;
			this.newsTopicRepository = newstopicRepository;
		}

		private void Save ( ) {
			unitwork.Save();
		}

		public int CreateNewsTopic ( NewsTopic topic ) {
			try {
				newsTopicRepository.Add(topic);
				Save();
				return topic.ID;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return -1;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="topic"></param>
		/// <returns></returns>
		public bool UpdateNewsTopic ( NewsTopic topic ) {
			try {
				newsTopicRepository.Update(topic);
				Save();
				return true;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public NewsTopic GetTopic ( int id ) {
			return newsTopicRepository.GetById(id);
		}

		public IEnumerable<NewsTopic> GetAllNewsTopics ( ) {
			return newsTopicRepository.GetAll();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pagesize"></param>
		/// <param name="pageindex"></param>
		/// <param name="isTotal"></param>
		/// <returns></returns>
		public PagedList<NewsTopic> GetNewsTopics ( int pagesize, int pageindex ) {
			return newsTopicRepository.GetNewsTopics(pagesize, pageindex);
		}
	}
}
