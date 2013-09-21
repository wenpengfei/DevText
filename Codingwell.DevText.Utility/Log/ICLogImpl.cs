using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Core;

namespace Codingwell.DevText.Utility {
	public class ICLogImpl : LogImpl, ICLog {

		private readonly static Type _DeclaringType = typeof(ICLogImpl);

		public ICLogImpl ( ILogger logger ) : base(logger) { }

		public void Info ( int userid, string clientip, string moduletype, string title, string summary, object message ) {
			Info( userid,clientip, moduletype, title, summary, message, null);
		}

		public void Info ( int userid, string clientip, string moduletype, string title, string summary, object message, Exception e ) {
			if (this.IsInfoEnabled) {
				LoggingEvent logevent = new LoggingEvent(_DeclaringType, Logger.Repository, Logger.Name, Level.Info, message, e);
				logevent.Properties["clientip"] = clientip;
				logevent.Properties["moduletype"] = moduletype;
				logevent.Properties["userid"] = userid;
				logevent.Properties["title"] = title;
				logevent.Properties["summary"] = summary;
				Logger.Log(logevent);
			}
		}

		public void Warn ( int userid, string clientip, string moduletype, string title, string summary, object message ) {
			Warn(userid, clientip, moduletype, title, summary, message, null);
		}

		public void Warn ( int userid, string clientip, string moduletype, string title, string summary, object message, Exception e ) {
			if (this.IsWarnEnabled) {
				LoggingEvent logevent = new LoggingEvent(_DeclaringType, Logger.Repository, Logger.Name, Level.Info, message, e);
				logevent.Properties["clientip"] = clientip;
				logevent.Properties["moduletype"] = moduletype;
				logevent.Properties["title"] = title;
				logevent.Properties["userid"] = userid;
				logevent.Properties["summary"] = summary;
				Logger.Log(logevent);
			}
		}

		public void Error ( int userid, string title, string summary, string clientip, string moduletype, object message ) {
			Error(userid,title, summary, clientip, moduletype, message, null);
		}

		public void Error ( int userid, string title, string summary, string clientip, string moduletype, object message, Exception e ) {
			if (this.IsErrorEnabled) {
				LoggingEvent logevent = new LoggingEvent(_DeclaringType, Logger.Repository, Logger.Name, Level.Info, message, e);
				logevent.Properties["clientip"] = clientip;
				logevent.Properties["moduletype"] = moduletype;
				logevent.Properties["title"] = title;
				logevent.Properties["userid"] = userid;
				logevent.Properties["summary"] = summary;
				Logger.Log(logevent);
			}
		}

		public void Fatal ( int userid, string title, string summary, string clientip, string moduletype, object message ) {
			Fatal(userid,title, summary, clientip, moduletype, message, null);
		}

		public void Fatal ( int userid, string title, string summary, string clientip, string moduletype, object message, Exception e ) {
			if (this.IsFatalEnabled) {
				LoggingEvent logevent = new LoggingEvent(_DeclaringType, Logger.Repository, Logger.Name, Level.Info, message, e);
				logevent.Properties["clientip"] = clientip;
				logevent.Properties["moduletype"] = moduletype;
				logevent.Properties["title"] = title;
				logevent.Properties["userid"] = userid;
				logevent.Properties["summary"] = summary;
				Logger.Log(logevent);
			}
		}

		public void Debug ( int userid, string title, string summary, string clientip, string moduletype, object message ) {
			Debug(userid,title, summary, clientip, moduletype, message, null);
		}
		public void Debug ( int userid, string title, string summary, string clientip, string moduletype, object message, Exception e ) {
			if (this.IsDebugEnabled) {
				LoggingEvent logevent = new LoggingEvent(_DeclaringType, Logger.Repository, Logger.Name, Level.Info, message, e);
				logevent.Properties["clientip"] = clientip;
				logevent.Properties["moduletype"] = moduletype;
				logevent.Properties["title"] = title;
				logevent.Properties["userid"] = userid;
				logevent.Properties["summary"] = summary;
				Logger.Log(logevent);
			}
		}
	}
}
