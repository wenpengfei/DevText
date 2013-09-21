using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Data.Infrastructure;

namespace Codingwell.DevText.Data {

	public interface IQuestionSupplementRepository : IRepository<QuestionSupplement> {

	}

	public class QuestionSupplementRepository : RepositoryBase<QuestionSupplement>, IQuestionSupplementRepository {

		public QuestionSupplementRepository( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {

		}
	}
}
