using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Data.Infrastructure;
using Webdiyer.WebControls.Mvc;

namespace Codingwell.DevText.Data {

	public interface IQuestionRepository : IRepository<Question> {
		PagedList<Question> GetQuestions ( int pagesize, int pageindex );
	}

	public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository {

		public QuestionRepository ( IDatabaseFactory databaseFactory )
			: base(databaseFactory) {

		}

		public PagedList<Question> GetQuestions ( int pagesize, int pageindex ) {
			if (pageindex < 1)
				pageindex = 1;
			return this.DB.Questions.OrderByDescending(a => a.OccurTime).ToPagedList(pageindex, pagesize);
		}
	}
}
