using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Data;
using Codingwell.DevText.Data.Infrastructure;
using log4net;

namespace Codingwell.DevText.Service {

	public interface IQuestionSupplementService {

	}

	public class QuestionSupplementService : IQuestionSupplementService {
		private readonly IQuestionSupplementRepository questionSupplementRepository;
		private readonly IUnitOfWork unitwork;
		//private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public QuestionSupplementService ( IUnitOfWork unitwork, IQuestionSupplementRepository questionSupplementRepository ) {
			this.unitwork = unitwork;
			this.questionSupplementRepository = questionSupplementRepository;
		}
	}
}
