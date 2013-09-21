using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Model.ViewModel {
	public class HomeViewModel {

		public HomeViewModel ( ) { }

		public PagedList<HomeContent> FoucsContent { get; set; }

		public PagedList<HomeContent> Articles { get; set; }

		public PagedList<HomeContent> News { get; set; }

		public PagedList<HomeContent> Links { get; set; }
	}
}
