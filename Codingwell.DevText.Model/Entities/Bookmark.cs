using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class Bookmark
	{
		public int ID { get; set; }
		public int CreatorID { get; set; }
		public string Title { get; set; }
		public string TargetUrl { get; set; }
		public string Remark { get; set; }
		public System.DateTime InsertTime { get; set; }
		public bool IsPrivate { get; set; }
		public int UpCount { get; set; }
		public int State { get; set; }
		public virtual User User { get; set; }
	}
}

