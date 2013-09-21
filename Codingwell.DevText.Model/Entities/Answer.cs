using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class Answer
	{
		public int ID { get; set; }
		public int QuestionID { get; set; }
		public int AnswerID { get; set; }
		public string Content { get; set; }
		public System.DateTime OccurTime { get; set; }
		public int State { get; set; }
		public int UpCount { get; set; }
		public int DownCount { get; set; }
		public virtual Question Question { get; set; }
		public virtual User User { get; set; }
	}
}

