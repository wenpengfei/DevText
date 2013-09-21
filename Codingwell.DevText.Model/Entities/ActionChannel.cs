using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class ActionChannel
	{
	    public ActionChannel()
		{
			this.Actions = new List<Action>();
		}

		public int ID { get; set; }
		public string Name { get; set; }
		public System.DateTime CreateTime { get; set; }
		public int Rank { get; set; }
		public int State { get; set; }
		public virtual ICollection<Action> Actions { get; set; }
	}
}

