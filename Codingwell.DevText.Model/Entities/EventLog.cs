using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class EventLog
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public System.DateTime OccurTime { get; set; }
		public string ClientIP { get; set; }
		public int EventType { get; set; }
		public string EventTitle { get; set; }
		public string EventSummary { get; set; }
	}
}

