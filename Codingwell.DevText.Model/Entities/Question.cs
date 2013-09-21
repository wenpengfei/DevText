using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class Question
	{
	    public Question()
		{
			this.Answers = new List<Answer>();
			this.QuestionAnswers = new List<QuestionAnswer>();
			this.QuestionSupplements = new List<QuestionSupplement>();
		}

		public int ID { get; set; }
		public int AskerID { get; set; }
		public string Path { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public string Content { get; set; }
		public System.DateTime OccurTime { get; set; }
		public int State { get; set; }
		public int DownCount { get; set; }
		public int UpCount { get; set; }
		public int FavouriteCount { get; set; }
		public virtual ICollection<Answer> Answers { get; set; }
		public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
		public virtual User User { get; set; }
		public virtual ICollection<QuestionSupplement> QuestionSupplements { get; set; }
	}
}

