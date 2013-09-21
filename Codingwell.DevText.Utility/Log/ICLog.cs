using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Codingwell.DevText.Utility {
	public interface ICLog : ILog {

		void Info ( int userid, string clientip, string moduletype, string title, string summary, object message );
		void Info ( int userid, string clientip, string moduletype, string title, string summary, object message, Exception e );

		void Warn ( int userid, string clientip, string moduletype, string title, string summary, object message );
		void Warn ( int userid, string clientip, string moduletype, string title, string summary, object message, Exception e );

		void Error ( int userid, string title, string summary, string clientip, string moduletype, object message );
		void Error ( int userid, string title, string summary, string clientip, string moduletype, object message, Exception e );

		void Fatal ( int userid, string title, string summary, string clientip, string moduletype, object message );
		void Fatal ( int userid, string title, string summary, string clientip, string moduletype, object message, Exception e );

		void Debug ( int userid, string title, string summary, string clientip, string moduletype, object message );
		void Debug ( int userid, string title, string summary, string clientip, string moduletype, object message, Exception e );
	}
}
