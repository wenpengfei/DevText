using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Data;
using Codingwell.DevText.Data.Infrastructure;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Service {

	public interface IEventlogService {

	}

	public class EventlogService : IEventlogService {
		private readonly IEventlogRepository eventlogRepository;
		private readonly IUnitOfWork unitwork;

		public EventlogService ( IEventlogRepository eventlogrepository, IUnitOfWork unitofwork ) {
			this.eventlogRepository = eventlogrepository;
			this.unitwork = unitofwork;
		}

		private void Save ( ) {
			unitwork.Save();
		}

		/// <summary>
		/// 创建日志
		/// </summary>
		/// <param name="log"></param>
		/// <returns></returns>
		public int CreateEventlog ( EventLog log ) {
			try {
				eventlogRepository.Add(log);
				Save();
				return log.ID;
			} catch {
				return -1;
			}
		}
		/// <summary>
		/// 更新日志
		/// </summary>
		/// <param name="log"></param>
		/// <returns></returns>
		public bool UpdateEventlog ( EventLog log ) {
			try {
				eventlogRepository.Update(log);
				Save();
				return true;
			} catch {
				return false;
			}
		}

		public PagedList<EventLog> GetEventlogs ( int pagesize, int pageindex ) {
			return eventlogRepository.GetEventlogs(pagesize, pageindex);
		}

		public PagedList<EventLog> GetEventlogs ( int pagesize, int pageindex, int userid ) {
			return eventlogRepository.GetEventlogs(pagesize, pageindex, userid);
		}
	}
}
