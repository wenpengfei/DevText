using System;
using System.Collections.Generic;

namespace Codingwell.DevText.Model.Entities
{
	public class SysLog
	{
		public int LogID { get; set; }
		public System.DateTime LogDate { get; set; }
		public string LogThread { get; set; }
		public string LogLevel { get; set; }
		public string Logger { get; set; }
		public string LogMessage { get; set; }
		public string LogException { get; set; }
	}
}

