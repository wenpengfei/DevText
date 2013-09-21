using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Data.Infrastructure;

namespace Codingwell.DevText.Data {

	public interface IOpenAuthRepository : IRepository<OpenAuth> {

		OpenAuth GetOpenAuthEntityByID ( int id );

		OpenAuth GetOpenAuthEntityByOpenID ( string openid );

		OpenAuth GetOpenAuthEntityByUserID ( int userid );
	}

	public class OpenAuthRepository : RepositoryBase<OpenAuth>, IOpenAuthRepository {
		public OpenAuthRepository ( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {
		}

		public override void Add ( OpenAuth entity ) {
			if (this.DB.OpenAuths.Where(q => q.OpenId == entity.OpenId).Count() > 0)
				return;
			this.DB.OpenAuths.Add(entity);
		}

		public OpenAuth GetOpenAuthEntityByID ( int id ) {
			return this.DB.OpenAuths.FirstOrDefault(q => q.ID == id);
		}

		public OpenAuth GetOpenAuthEntityByOpenID ( string openid ) {
			return this.DB.OpenAuths.FirstOrDefault(q => q.OpenId == openid);
		}

		public OpenAuth GetOpenAuthEntityByUserID ( int userid ) {
			return this.DB.OpenAuths.FirstOrDefault(q => q.UserID == userid);
		}
	}
}
