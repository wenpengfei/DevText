using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class ArticleCategory
	{
	    public ArticleCategory()
		{
			this.Articles = new List<Article>();
		}

		public int ID { get; set; }
		public int OwnerID { get; set; }
		public string Name { get; set; }
		public string ImgPath { get; set; }
		public System.DateTime CreateTime { get; set; }
		public System.DateTime LastChanged { get; set; }
		public int State { get; set; }
		public virtual User User { get; set; }
		public virtual ICollection<Article> Articles { get; set; }
	}
}

