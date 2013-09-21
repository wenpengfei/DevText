using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data;
using System.Data.Entity;

namespace Codingwell.DevText.Data.Infrastructure {
	public abstract class RepositoryBase<T> where T : class {
		private DevTextDbContext db;
		private readonly IDbSet<T> dbset;

		public RepositoryBase ( IDatabaseFactory databaseFactory ) {
			DatabaseFactory = databaseFactory;
			dbset = DB.Set<T>();
		}

		protected IDatabaseFactory DatabaseFactory {
			get;
			private set;
		}

		protected DevTextDbContext DB {
			get { return db ?? ( db = DatabaseFactory.GetDb() ); }
		}

		public virtual void Add ( T entity ) {
			dbset.Add(entity);
		}

		public virtual void Update ( T entity ) {
			dbset.Attach(entity);
			db.Entry(entity).State = EntityState.Modified;
		}

		public virtual void Delete ( T entity ) {
			dbset.Remove(entity);
		}

		public virtual void Delete ( Expression<Func<T, bool>> where ) {
			IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
			foreach (T obj in objects)
				dbset.Remove(obj);
		}

		public virtual T GetById ( long Id ) {
			return dbset.Find(Id);
		}

		public virtual T GetById ( string Id ) {
			return dbset.Find(Id);
		}

		public virtual T Get ( Expression<Func<T, bool>> where ) {
			var query = dbset.Where<T>(where);
			if (query.Count() > 0)
				return query.First();
			return null;
		}

		public virtual IEnumerable<T> GetAll ( ) {
			return dbset.ToList();
		}

		public virtual IEnumerable<T> GetMany ( Expression<Func<T, bool>> where ) {
			return dbset.Where(where).ToList();
		}

	}
}
