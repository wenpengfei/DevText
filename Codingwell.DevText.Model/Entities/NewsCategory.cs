using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class NewsCategory
	{
	    public NewsCategory()
		{
			this.News = new List<News>();
		}

		public int ID { get; set; }
		public Nullable<int> ParentID { get; set; }
		public string Name { get; set; }
		public string ImgPath { get; set; }
		public System.DateTime CreateTime { get; set; }
		public System.DateTime LastChanged { get; set; }
		public int State { get; set; }
		public virtual ICollection<News> News { get; set; }
	}
}

