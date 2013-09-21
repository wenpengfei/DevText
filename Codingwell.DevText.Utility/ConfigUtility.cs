using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Codingwell.DevText.Utility {
	public class ConfigUtility {
		/// <summary>
		/// 获取数据访问层类型	
		/// </summary>
		/// <returns></returns>
		public static string GetDaoTypeString ( ) {
			string daoStr = ConfigurationManager.AppSettings["DaoType"];
			if (string.IsNullOrEmpty(daoStr)) {
				throw new Exception("未配置数据访问层类型，请检查Config文件的DaoType节点。");
			} else {
				return daoStr;
			}
		}

		public static string GetAppSettingValue ( string key ) {
			string keyvalue = ConfigurationManager.AppSettings[key];
			if (string.IsNullOrEmpty(keyvalue)) {
				throw new Exception(string.Format("未找到配置项 {0},请检查配置文件",key));
			} else {
				return keyvalue;
			}
		}
	}
}
