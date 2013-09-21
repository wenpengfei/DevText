using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;

namespace Codingwell.DevText.Model.ViewModel {
	public class ArticleDetail {
		public ArticleDetail ( ) {

		}

		public Article Article {
			get;
			set;
		}

		public User Owner {
			get;
			set;
		}
	}
}
