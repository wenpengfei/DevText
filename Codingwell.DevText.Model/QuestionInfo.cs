using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codingwell.DevText.Model {
	public class QuestionInfo {
	}

	public class AnswerInfo {

		public AnswerInfo ( ) {

		}

		public static AnswerInfo LoadFromEntity ( AnswerEntity entity ) {
			if (entity == null)
				return default(AnswerInfo);
			AnswerInfo info = new AnswerInfo() {
				ID = entity.ID,
				Content = entity.Content,
				OccurTime = entity.OccurTime,
				State = (DbState)entity.State,
				UpCount = entity.UpCount,
				DownCount = entity.DownCount
			};
			return info;
		}

		public int ID { get; set; }

		public QuestionInfo Question { get; set; }
		/// <summary>
		/// 回答人
		/// </summary>
		public UserInfo Answer { get; set; }

		public string Content { get; set; }

		public DateTime OccurTime { get; set; }

		public DbState State { get; set; }

		public int UpCount { get; set; }

		public int DownCount { get; set; }
	}
}
