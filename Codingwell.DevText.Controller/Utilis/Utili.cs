using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Enumeration;

namespace Codingwell.DevText.Controller {
	public class Utili {
		/// <summary>
		/// 获取状态字符串
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public static string GetStateString ( int state ) {
			switch (state) {
				case (int)RecordState.Approved:
					return "正常";
				case (int)RecordState.UnPassed:
					return "审批未通过";
				case (int)RecordState.Pending:
					return "待审批";
				case (int)RecordState.Draft:
					return "草稿";
				case (int)RecordState.Deleted:
					return "已删除";
				default :
					return "";
			}
		}

		/// <summary>
		/// 获取账户状态字符串
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public static string GetAccountStateString ( int state ) {
			switch (state) {
				case (int)AccountState.Normal:
					return "正常";
				case (int)AccountState.Deleted:
					return "已删除";
				case (int)AccountState.Forbidden:
					return "锁定";
				default:
					return "";
			}
		}
	}
}
