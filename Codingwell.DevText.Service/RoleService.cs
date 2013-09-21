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

	public interface IRoleService {
		bool CreateRole ( Role entity );
		bool UpdateRole ( Role entity );
		PagedList<Role> GetRoles ( int pagesize, int pageindex, string keyword );
		IEnumerable<Role> GetUserRoles ( int userid );
		IEnumerable<Codingwell.DevText.Model.Entities.Action> GetUserActions ( int userid );
	}

	public class RoleService : IRoleService {
		private readonly IRoleRepository roleRepository;
		private readonly IUnitOfWork unitOfWork;
		//private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public RoleService ( IUnitOfWork unitwork, IRoleRepository roleRepository ) {
			this.unitOfWork = unitwork;
			this.roleRepository = roleRepository;
		}
		private void Save ( ) {
			unitOfWork.Save();
		}

		public bool CreateRole ( Role entity ) {
			try {
				roleRepository.Add(entity);
				Save();
				return true;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return false;
			}
		}
		public bool UpdateRole ( Role entity ) {
			try {
				roleRepository.Update(entity);
				Save();
				return true;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return false;
			}
		}
		public PagedList<Role> GetRoles ( int pagesize, int pageindex, string keyword ) {
			return roleRepository.GetRoles(pagesize, pageindex, keyword);
		}

		public IEnumerable<Role> GetUserRoles ( int userid ) {
			return roleRepository.GetUserRoles(userid);
		}
		public IEnumerable<Codingwell.DevText.Model.Entities.Action> GetUserActions ( int userid ) {
			return roleRepository.GetUserActions(userid);
		}
	}
}
