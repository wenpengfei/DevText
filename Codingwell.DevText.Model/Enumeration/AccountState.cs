using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codingwell.DevText.Model.Enumeration {
	/// <summary>
	/// 账户状态
	/// </summary>
	public enum AccountState {
		/// <summary>
		/// 无
		/// </summary>
		None = 0,
		/// <summary>
		/// 已删除
		/// </summary>
		Deleted,
		/// <summary>
		/// 限制登录
		/// </summary>
		Forbidden,
		/// <summary>
		/// 正常
		/// </summary>
		Normal,
		/// <summary>
		/// 待激活
		/// </summary>
		PendingActived
	}
}
