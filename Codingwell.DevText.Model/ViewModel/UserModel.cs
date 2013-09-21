using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Model.ViewModel {
	public class UserHomeModel {

		public User CurrentUser { get; set; }

		public PagedList<News> News { get; set; }

		public PagedList<Question> Questions { get; set; }

		public PagedList<Bookmark> Bookmarks { get; set; }

		public PagedList<Article> Articles { get; set; }
	}
}
