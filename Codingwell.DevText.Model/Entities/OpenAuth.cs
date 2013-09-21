using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class OpenAuth
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public string OpenId { get; set; }
		public string AccessTokenKey { get; set; }
		public string AccessTokenSecret { get; set; }
		public System.DateTime InsertTime { get; set; }
		public virtual User User { get; set; }
		public int OpenAuthType { get; set; }
	}
}

