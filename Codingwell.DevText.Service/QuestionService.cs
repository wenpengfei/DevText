using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Data;
using Codingwell.DevText.Data.Infrastructure;
using Webdiyer.WebControls.Mvc;
using log4net;

namespace Codingwell.DevText.Service {

	public interface IQuestionService {
		PagedList<Question> GetQuestions ( int pagesize, int pageindex );
	}

	public class QuestionService : IQuestionService {
		private readonly IQuestionRepository questionRepository;
		private readonly IUnitOfWork unitwork;
		//private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public QuestionService (IUnitOfWork unitwork,IQuestionRepository questionRepository ) {
			this.unitwork = unitwork;
			this.questionRepository = questionRepository;
		}

		public PagedList<Question> GetQuestions ( int pagesize, int pageindex ) {
			return questionRepository.GetQuestions(pagesize, pageindex);
		}
	}
}
