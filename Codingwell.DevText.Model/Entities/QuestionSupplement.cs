using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class QuestionSupplement
	{
		public int ID { get; set; }
		public int QuestionID { get; set; }
		public int AddUserID { get; set; }
		public string Content { get; set; }
		public System.DateTime AddTime { get; set; }
		public virtual Question Question { get; set; }
		public virtual User User { get; set; }
	}
}

