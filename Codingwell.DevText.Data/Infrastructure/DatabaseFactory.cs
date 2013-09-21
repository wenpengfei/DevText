using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codingwell.DevText.Data.Infrastructure {
	public class DatabaseFactory : Disposable, IDatabaseFactory {
		private DevTextDbContext db;

		public DevTextDbContext GetDb ( ) {
			return db ?? ( db = new DevTextDbContext() );
		}

		protected override void DisposeCore ( ) {
			if (db != null)
				db.Dispose();
		}
	}
}
