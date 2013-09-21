using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Data;
using Codingwell.DevText.Data.Infrastructure;
using log4net;

namespace Codingwell.DevText.Service {

	public interface IQuestionAnswerService {

	}

	public class QuestionAnswerService : IQuestionAnswerService {
		private readonly IQuestionAnswerRepository questionAnswerRepository;
		private readonly IUnitOfWork unitwork;
		//private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public QuestionAnswerService ( IUnitOfWork unitwork,IQuestionAnswerRepository questionAnswerRepository) {
			this.unitwork = unitwork;
			this.questionAnswerRepository = questionAnswerRepository;
		}
	}
}
