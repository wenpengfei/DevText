using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codingwell.DevText.Utility {
	public static class StringHelper {
		/// <summary>
		/// 转换为整数
		/// </summary>
		/// <param name="str"></param>
		/// <returns>0 or number</returns>
		public static Int32 ToInt32 ( this string str ) {
			int result;
			if (int.TryParse(str, out result))
				return result;
			return 0;
		}

		/// <summary>
		/// 转换为长整数
		/// </summary>
		/// <param name="str"></param>
		/// <returns>0 or number</returns>
		public static Int64 ToInt64 ( this string str ) {
			Int64 result;
			if (Int64.TryParse(str, out result))
				return result;
			return 0L;
		}

	}
}
