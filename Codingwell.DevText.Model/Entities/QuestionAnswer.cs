using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class QuestionAnswer
	{
		public int ID { get; set; }
		public int QuestionID { get; set; }
		public int AnswerID { get; set; }
		public string Content { get; set; }
		public System.DateTime AnswerTime { get; set; }
		public int Useful { get; set; }
		public int UnUseful { get; set; }
		public int CommentsCount { get; set; }
		public virtual Question Question { get; set; }
		public virtual User User { get; set; }
	}
}

