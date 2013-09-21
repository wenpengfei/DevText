using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codingwell.DevText.Data;
using Codingwell.DevText.Data.Infrastructure;
using Codingwell.DevText.Model.Entities;
using log4net;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Service {

	public interface IActionService {
		bool CreateActionChannel ( ActionChannel entity );
		bool UpdateActionChannel ( ActionChannel entity );
		bool CreateAction ( Codingwell.DevText.Model.Entities.Action entity );
		bool UpdateAction ( Codingwell.DevText.Model.Entities.Action entity );
		IEnumerable<ActionChannel> GetAllActionChannels ( );
		PagedList<ActionChannel> GetActionChannels ( int pagesize, int pageindex, string keyword );
		ActionChannel GetActionChannel ( int channelid );
		PagedList<Codingwell.DevText.Model.Entities.Action> GetActions ( int pagesize, int pageindex, string keyword );
		Codingwell.DevText.Model.Entities.Action GetAction ( int id );
	}

	public class ActionService : IActionService {
		private readonly IActionRepository actionRepository;
		private readonly IActionChannelRepository actionChannelRepository;
		private readonly IUnitOfWork unitwork;
		//private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public ActionService ( IUnitOfWork unitwork, IActionRepository actionRepository, IActionChannelRepository actionChannelRepository ) {
			this.unitwork = unitwork;
			this.actionRepository = actionRepository;
			this.actionChannelRepository = actionChannelRepository;
		}

		private void Save ( ) {
			unitwork.Save();
		}

		public bool CreateActionChannel ( ActionChannel entity ) {
			try {
				actionChannelRepository.Add(entity);
				Save();
				return true;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return false;
			}
		}

		public bool UpdateActionChannel ( ActionChannel entity ) {
			try {
				actionChannelRepository.Update(entity);
				Save();
				return true;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return false;
			}
		}

		public bool CreateAction ( Codingwell.DevText.Model.Entities.Action entity ) {
			try {
				actionRepository.Add(entity);
				Save();
				return true;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return false;
			}
		}

		public bool UpdateAction ( Codingwell.DevText.Model.Entities.Action entity ) {
			try {
				actionRepository.Update(entity);
				Save();
				return true;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return false;
			}
		}

		public IEnumerable<ActionChannel> GetAllActionChannels ( ) {
			return actionChannelRepository.GetAll();
		}

		public PagedList<ActionChannel> GetActionChannels ( int pagesize, int pageindex, string keyword ) {
			return actionChannelRepository.GetActionChannels(pagesize, pageindex, keyword);
		}

		public ActionChannel GetActionChannel ( int channelid ) {
			return actionChannelRepository.GetById(channelid);
		}

		public PagedList<Codingwell.DevText.Model.Entities.Action> GetActions ( int pagesize, int pageindex, string keyword ) {
			return actionRepository.GetActions(pagesize, pageindex, keyword);
		}

		public Codingwell.DevText.Model.Entities.Action GetAction ( int id ) {
			return actionRepository.GetById(id);
		}
	}
}
