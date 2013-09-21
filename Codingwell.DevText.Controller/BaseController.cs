using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using Codingwell.DevText.Model;
using Codingwell.DevText.Utility;
using Codingwell.Utility;

namespace Codingwell.DevText.Controller {
	public class BaseController : System.Web.Mvc.Controller {
		public BaseController ( ) {

		}
		/// <summary>
		/// 获取Pagesize
		/// </summary>
		/// <returns></returns>
		protected int GetPageSize ( ) {
			if (string.IsNullOrEmpty(Request.QueryString["pagesize"]))
				return 10;
			else
				return Request.QueryString["pagesize"].ToInt32();
		}
		/// <summary>
		/// 获取当前页面
		/// </summary>
		/// <returns></returns>
		protected int GetPageIndex ( ) {
			if (string.IsNullOrEmpty(Request.QueryString["page"])) {
				if (string.IsNullOrEmpty(Request.QueryString["pageindex"]))
					return 1;
				else
					return Request.QueryString["pageindex"].ToInt32();
			} else
				return Request.QueryString["page"].ToInt32();
		}
	}
}
