using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class Role
	{
	    public Role()
		{
			this.Actions = new List<Action>();
			this.Users = new List<User>();
		}

		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public System.DateTime CreateTime { get; set; }
		public int State { get; set; }
		public virtual ICollection<Action> Actions { get; set; }
		public virtual ICollection<User> Users { get; set; }
	}
}

