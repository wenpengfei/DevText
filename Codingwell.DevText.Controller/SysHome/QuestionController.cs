using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model;
using Codingwell.DevText.Service;

namespace Codingwell.DevText.Controller.SysHome {
	public class QuestionController : AuthBaseController {
		private readonly IQuestionService questionService;

		public QuestionController ( IQuestionService questionservice ) {
			this.questionService = questionservice;
		}

		[HttpGet]
		public ActionResult Index ( ) {
			int pagesize = GetPageSize();
			int pageindex = GetPageIndex();
			return View(questionService.GetQuestions(pagesize, pageindex));
		}
	}
}
