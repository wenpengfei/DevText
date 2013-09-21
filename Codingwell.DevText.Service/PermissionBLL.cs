using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codingwell.DevText.Model.Entities;
using Codingwell.DevText.Utility;

namespace Codingwell.DevText.BLL {
	//public class PermissionBLL {
	//    private const string CACHE_PERMISSION_RULE = "CACHE_PERMISSION_RULE";

	//    private PermissionRule GetPermissionRuleOfUser ( int userid ) {
	//        PermissionRule permissionRule = (PermissionRule)CacheManage.GetFromCache(CACHE_PERMISSION_RULE + "_" + userid);
	//        if (permissionRule != null) {
	//            return permissionRule;
	//        }
	//        //User user = BLLFactory<UserService>.Instance.GetUser(userid);
	//        PermissionRule userrule = new PermissionRule();
	//        //IList<Action> useractions = BLLFactory<RoleService>.Instance.GetUserActions(userid).ToList();
	//        //foreach (Action action in useractions) {
	//        //    PermissionRule.DataAccessRule daRule = new PermissionRule.DataAccessRule();
	//        //    daRule.ActionID = action.ID;
	//        //    daRule.ActionValue = action.ActionValue;
	//        //    userrule.AddDataAccessRule(daRule);
	//        //}
	//        //CacheManage.SetCache(CACHE_PERMISSION_RULE + "_" + userid, userrule);
	//        return userrule;
	//    }

	//    /// <summary>
	//    /// 验证是否有权限
	//    /// </summary>
	//    /// <param name="userid"></param>
	//    /// <param name="actionvalue"></param>
	//    /// <returns></returns>
	//    public bool HasActionPermission ( int userid, string actionvalue ) {
	//        PermissionRule userrule = GetPermissionRuleOfUser(userid);
	//        if (userrule.DataAccessRules.Where(u => u.ActionValue == actionvalue).Count() > 0)
	//            return true;
	//        else
	//            return false;
	//    }
	//}

	//internal class PermissionRule {

	//    public class DataAccessRule {
	//        public int ActionID { get; set; }
	//        public string ActionValue { get; set; }
	//    }

	//    public List<DataAccessRule> DataAccessRules = new List<DataAccessRule>();

	//    public void AddDataAccessRule ( DataAccessRule rule ) {
	//        DataAccessRules.Add(rule);
	//    }
	//}
}
