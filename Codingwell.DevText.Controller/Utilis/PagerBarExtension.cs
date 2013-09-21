using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace System.Web.Mvc {
	public enum BarStyle {
		yahoo,
		digg,
		meneame,
		flickr,
		sabrosus,
		scott,
		quotes,
		black,
		black2,
		grayr,
		yellow,
		jogger,
		starcraft2,
		tres,
		megas512,
		technorati,
		youtube,
		msdn,
		badoo,
		viciao,
		yahoo2,
		green_black
	}

	public static class PagerBarExtension {

		public static HtmlString RenderPagerBar ( this HtmlHelper html, int page, int total ) {
			return RenderPagerBar(html, page, total, BarStyle.technorati);
		}

		public static HtmlString RenderPagerBar ( this HtmlHelper html, int page, int total, BarStyle style ) {
			return RenderPagerBar(html, page, total, style, total);
		}

		public static HtmlString RenderPagerBar ( this HtmlHelper html, int page, int total, BarStyle style, int show ) {
			if (total == 1) {
				return new HtmlString(string.Empty);
			} else {
				StringBuilder sb = new StringBuilder();
				string _path = html.ViewContext.HttpContext.Request.Path;
				sb.Append("<div class=\"");
				sb.Append(style.ToString());
				sb.Append("\" >");

				string queryString = html.ViewContext.HttpContext.Request.QueryString.ToString();
				if (queryString.IndexOf("page=") < 0) {
					queryString += "page=" + page;
				}
				Regex re = new Regex(@"page=\d+", RegexOptions.IgnoreCase);
				string result = re.Replace(queryString, "page={0}");

				if (page != 1) {
					sb.AppendFormat("<span><a href=\"{0}\" title=\"第一页\">{1}</a></span>", _path + "?" + string.Format(result, 1), "<<");
					sb.AppendFormat("<span><a href=\"{0}\" title=\"上一页\">{1}</a></span>", _path + "?" + string.Format(result, page - 1), "<");
				}
				if (page > ( show + 1 )) {
					sb.AppendFormat("<span><a href=\"{0}\" title=\"前" + ( show + 1 ) + "页\">{1}</a></span>", _path + "?" + string.Format(result, page - ( show + 1 )), "..");

				}
				for (int i = page - show; i <= page + show; i++) {
					if (i == page) {
						sb.AppendFormat("<span class=\"current\">{0}</span>", i);
					} else {
						if (i > 0 & i <= total) {
							sb.AppendFormat("<span><a href=\"{0}\">{1}</a></span>", _path + "?" + string.Format(result, i), i);
						}
					}
				}
				if (page < ( total - ( show ) )) {
					sb.AppendFormat("<span><a href=\"{0}\" title=\"后" + ( show + 1 ) + "页\">{1}</a></span>", _path + "?" + string.Format(result, page + ( show + 1 )), "..");

				}
				if (page < total) {
					sb.AppendFormat("<span><a href=\"{0}\" title=\"下一页\">{1}</a></span>", _path + "?" + string.Format(result, page + 1), ">");
					sb.AppendFormat("<span><a href=\"{0}\" title=\"最后一页\">{1}</a></span>", _path + "?" + string.Format(result, total), ">>");

				}
				sb.AppendFormat("<span class=\"current\">共{1}页</span>", page, total);
				sb.Append("</div>");
				return new HtmlString(sb.ToString());
			}
		}
	}
}
