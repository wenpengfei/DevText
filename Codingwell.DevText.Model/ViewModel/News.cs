using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Model.ViewModel {


	public class NewsOfCategory {

		public NewsOfCategory ( ) { }

		public PagedList<News> News { get; set; }

		public NewsCategory Category { get; set; }
	}


	public class NewsOfTopic {

		public NewsOfTopic ( ) { }

		public PagedList<News> News { get; set; }

		public NewsTopic Topic { get; set; }

	}
}
