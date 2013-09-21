using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Data;
using Codingwell.DevText.Data.Infrastructure;

namespace Codingwell.DevText.Service {

	public interface IBuildLogService {
		IEnumerable<BuildLog> GetBuildLogs ( );
	}

	public class BuildLogService : IBuildLogService {
		private readonly IBuildLogRepository buildLogRepository;
		private readonly IUnitOfWork unitwork;

		public BuildLogService (IUnitOfWork unitwork,IBuildLogRepository buildlogRepository ) {
			this.unitwork = unitwork;
			this.buildLogRepository = buildlogRepository;
		}

		private void Save ( ) {
			unitwork.Save();
		}
		/// <summary>
		/// 获取开发日志
		/// </summary>
		/// <returns></returns>
		public IEnumerable<BuildLog> GetBuildLogs ( ) {
			return buildLogRepository.GetAll();
		}
	}
}
