using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codingwell.DevText.Model.Enumeration {

	public enum RecordState {
		/// <summary>
		/// 无
		/// </summary>
		None = 0,
		/// <summary>
		/// 已删除
		/// </summary>
		Deleted,
		/// <summary>
		/// 草稿
		/// </summary>
		Draft,
		/// <summary>
		/// 待审批
		/// </summary>
		Pending,
		/// <summary>
		/// 审批未通过
		/// </summary>
		UnPassed,
		/// <summary>
		/// 
		/// </summary>
		Approved
	}
}
