using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Data;
using Codingwell.DevText.Data.Infrastructure;
using log4net;

namespace Codingwell.DevText.Service {
	public interface IOpenAuthService {
		int CreateOpenAuth ( OpenAuth auth );
		void UpdateOpenAuth ( OpenAuth auth );
		OpenAuth GetOpenAuthEntityByID ( int id );
		OpenAuth GetOpenAuthEntityByOpenID ( string openid );
		OpenAuth GetOpenAuthEntityByUserID ( int userid );
	}

	public class OpenAuthService : IOpenAuthService {
		private readonly IOpenAuthRepository openauthRepository;
		private readonly IUnitOfWork unitwork;
		//private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public OpenAuthService ( IUnitOfWork unitwork, IOpenAuthRepository openAuthRepository ) {
			this.unitwork = unitwork;
			this.openauthRepository = openAuthRepository;
		}

		private void Save ( ) {
			unitwork.Save();
		}

		public int CreateOpenAuth ( OpenAuth auth ) {
			try {
				openauthRepository.Add(auth);
				Save();
				return auth.ID;
			} catch (Exception ex) {
				//log.Error(ex.Message, ex);
				return -1;
			}
		}

		public void UpdateOpenAuth ( OpenAuth auth ) {
			try {
				openauthRepository.Update(auth);
				Save();
			} catch (Exception ex) {

			}
		}

		public OpenAuth GetOpenAuthEntityByID ( int id ) {
			return openauthRepository.GetOpenAuthEntityByID(id);
		}

		public OpenAuth GetOpenAuthEntityByOpenID ( string openid ) {
			return openauthRepository.GetOpenAuthEntityByOpenID(openid);
		}

		public OpenAuth GetOpenAuthEntityByUserID ( int userid ) {
			return openauthRepository.GetOpenAuthEntityByUserID(userid);
		}
	}
}
