using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class Comment
	{
	    public Comment()
		{
			this.Articles = new List<Article>();
			this.News = new List<News>();
		}

		public int ID { get; set; }
		public string ModuleType { get; set; }
		public int CreatorID { get; set; }
		public string CommentContent { get; set; }
		public int UpCount { get; set; }
		public int DownCount { get; set; }
		public int ReportCount { get; set; }
		public System.DateTime InsertTime { get; set; }
		public int State { get; set; }
		public virtual ICollection<Article> Articles { get; set; }
		public virtual ICollection<News> News { get; set; }
	}
}

