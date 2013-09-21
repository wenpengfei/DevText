using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class NewsTopic
	{
	    public NewsTopic()
		{
			this.News = new List<News>();
		}

		public int ID { get; set; }
		public string Name { get; set; }
		public string FilePath { get; set; }
		public string SortKey { get; set; }
		public int State { get; set; }
		public virtual ICollection<News> News { get; set; }
	}
}

