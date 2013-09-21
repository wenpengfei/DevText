using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class Article
	{
	    public Article()
		{
			this.ArticleCategories = new List<ArticleCategory>();
			this.Comments = new List<Comment>();
		}

		public int ID { get; set; }
		public int CreatorID { get; set; }
		public string Path { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public string ArticleContent { get; set; }
		public System.DateTime CreateTime { get; set; }
		public System.DateTime LastChanged { get; set; }
		public int State { get; set; }
		public int PageViewCount { get; set; }
		public int RssViewCount { get; set; }
		public int UpCount { get; set; }
		public int DownCount { get; set; }
		public bool EnableComment { get; set; }
		public bool AnymousComment { get; set; }
		public string WebFrom { get; set; }
		public string WebFromAddress { get; set; }
		public string MetaKeywords { get; set; }
		public string MetaDescription { get; set; }
		public virtual User User { get; set; }
		public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
	}
}

