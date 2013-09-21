using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Codingwell.DevText.Utility {
	public class CacheManage {
		/// <summary>
		/// 从缓存获取
		/// </summary>
		/// <param name="objectkey"></param>
		/// <returns></returns>
		public static object GetFromCache ( string objectkey ) {
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			return objCache[objectkey];
		}
		/// <summary>
		/// 添加到缓存
		/// </summary>
		/// <param name="objectkey"></param>
		/// <param name="objObject"></param>
		public static void SetCache ( string objectkey, object objObject ) {
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			objCache.Insert(objectkey, objObject);
		}

		public static void RemoveFromCache ( string objectkey ) {
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			objCache.Remove(objectkey);
		}
	}
}
