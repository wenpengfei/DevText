using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codingwell.DevText.Data.Infrastructure {
	public class UnitOfWork : IUnitOfWork {
		private readonly IDatabaseFactory databaseFactory;
		private DevTextDbContext dataContext;

		public UnitOfWork ( IDatabaseFactory databaseFactory ) {
			this.databaseFactory = databaseFactory;
		}

		protected DevTextDbContext DataContext {
			get { return dataContext ?? ( dataContext = databaseFactory.GetDb() ); }
		}

		public void Save ( ) {
			DataContext.SaveChanges();
		}

	}
}
