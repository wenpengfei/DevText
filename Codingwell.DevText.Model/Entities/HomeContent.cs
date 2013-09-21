using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class HomeContent
	{
		public int ID { get; set; }
		public int ContentType { get; set; }
		public string Title { get; set; }
		public string LinkUrl { get; set; }
		public string ContentData { get; set; }
		public System.DateTime InsertTime { get; set; }
	}
}

