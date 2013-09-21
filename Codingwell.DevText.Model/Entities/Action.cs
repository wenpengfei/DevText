using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class Action
	{
	    public Action()
		{
			this.Roles = new List<Role>();
		}

		public int ID { get; set; }
		public int ChannelID { get; set; }
		public string ActionName { get; set; }
		public string ActionValue { get; set; }
		public string PageUrl { get; set; }
		public System.DateTime CreateTime { get; set; }
		public int State { get; set; }
		public virtual ActionChannel ActionChannel { get; set; }
		public virtual ICollection<Role> Roles { get; set; }
	}
}

