using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Data.Infrastructure;

namespace Codingwell.DevText.Data {
	
	public interface IBuildLogRepository : IRepository<BuildLog> {

	}

	public class BuildLogRepository : RepositoryBase<BuildLog>, IBuildLogRepository {

		public BuildLogRepository ( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {

		}
	}
}
