using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codingwell.DevText.Model.Enumeration {
	public enum SysRoles {

		None=0,
		/// <summary>
		/// 系统管理员
		/// </summary>
		SysAdmin=1,
		/// <summary>
		/// 系统编辑
		/// </summary>
		SysEditor=2,
		/// <summary>
		/// 新闻编辑
		/// </summary>
		NewsEditor=3
	}
}
