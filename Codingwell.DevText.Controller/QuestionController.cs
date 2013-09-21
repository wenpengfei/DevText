using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Codingwell.DevText.Model;
using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Model.Enumeration;
using Codingwell.DevText.Service;

namespace Codingwell.DevText.Controller {
	public class QuestionController : BaseController {
		private readonly IQuestionService questionService;

		public QuestionController ( IQuestionService questionservice ) {
			this.questionService = questionservice;
		}

		[HttpGet]
		public ActionResult Index ( ) {
			int pageindex = GetPageIndex();
			int pagesize = GetPageSize();
			return View(questionService.GetQuestions(pagesize,pageindex));
		}

		[HttpGet]
		public ActionResult Detail ( ) {
			return View();
		}
	}
}
