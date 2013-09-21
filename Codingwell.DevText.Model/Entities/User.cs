using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class User
	{
	    public User()
		{
			this.Answers = new List<Answer>();
			this.ArticleCategories = new List<ArticleCategory>();
			this.Articles = new List<Article>();
			this.Bookmarks = new List<Bookmark>();
			this.News = new List<News>();
			this.OpenAuths = new List<OpenAuth>();
			this.QuestionAnswers = new List<QuestionAnswer>();
			this.Questions = new List<Question>();
			this.QuestionSupplements = new List<QuestionSupplement>();
			this.Roles = new List<Role>();
		}

		public int UserID { get; set; }
		public string NickName { get; set; }
		public string Figureurl { get; set; }
		public string Figureurl50 { get; set; }
		public string Figureurl100 { get; set; }
		public string Email { get; set; }
		public string MSN { get; set; }
		public Nullable<int> QQ { get; set; }
		public System.DateTime CreateTime { get; set; }
		public System.DateTime LastLoginTime { get; set; }
		public string LastLoginIP { get; set; }
		public int OpenAuthType { get; set; }
		public int AccountState { get; set; }
		public string ActiveCode { get; set; }
		public virtual ICollection<Answer> Answers { get; set; }
		public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
		public virtual ICollection<Article> Articles { get; set; }
		public virtual ICollection<Bookmark> Bookmarks { get; set; }
		public virtual ICollection<News> News { get; set; }
		public virtual ICollection<OpenAuth> OpenAuths { get; set; }
		public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
		public virtual ICollection<Question> Questions { get; set; }
		public virtual ICollection<QuestionSupplement> QuestionSupplements { get; set; }
		public virtual ICollection<Role> Roles { get; set; }
	}
}

