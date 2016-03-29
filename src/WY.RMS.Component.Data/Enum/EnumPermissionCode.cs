/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2016/3/23 11:33:37  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.RMS.Component.Data.Enum
{
    /// <summary>
    /// 权限编码
    /// </summary>
    [Flags]
    public enum EnumPermissionCode
    {
        #region 角色管理
        /// <summary>
        /// 查询按钮（角色管理）
        /// </summary>
        QueryRole = 1,
        /// <summary>
        /// 新增按钮（角色管理）
        /// </summary>
        AddRole = 2,
        /// <summary>
        /// 修改按钮（角色管理）
        /// </summary>
        UpdateRole = 4,
        /// <summary>
        /// 删除按钮（角色管理）
        /// </summary>
        DeleteRole = 8,
        /// <summary>
        /// 授权按钮（角色管理）
        /// </summary>
        AuthorizeRole = 16,

        #endregion

        #region 用户管理
        /// <summary>
        /// 查询按钮（用户管理）
        /// </summary>
        QueryUser = 32,
        /// <summary>
        /// 新增按钮（用户管理）
        /// </summary>
        AddUser = 64,
        /// <summary>
        /// 修改按钮（用户管理）
        /// </summary>
        UpdateUser = 128,
        /// <summary>
        /// 删除按钮（用户管理）
        /// </summary>
        DeleteUser = 256,
        /// <summary>
        /// 重置密码按钮（用户管理）
        /// </summary>
        ResetPwdUser = 512,
        /// <summary>
        /// 设置用户组按钮（用户管理）
        /// </summary>
        SetGroupUser = 1024,
        /// <summary>
        /// 设置角色按钮（用户管理）
        /// </summary>
        SetRolesUser = 2048,
        #endregion

        #region 模块管理
        /// <summary>
        /// 查询按钮（模块管理）
        /// </summary>
        QueryModule = 4096,
        /// <summary>
        /// 新增按钮（模块管理）
        /// </summary>
        AddModule = 8192,
        /// <summary>
        /// 修改按钮（模块管理）
        /// </summary>
        UpdateModule = 16384,
        #endregion

        #region 权限管理
        /// <summary>
        /// 查询按钮（权限管理）
        /// </summary>
        QueryPermission = 32768,
        /// <summary>
        /// 新增按钮（权限管理）
        /// </summary>
        AddPermission = 65536,
        /// <summary>
        /// 修改按钮（权限管理）
        /// </summary>
        UpdatePermission = 131072,
        #endregion

        #region 操作日志管理
        /// <summary>
        /// 查询按钮（操作日志管理）
        /// </summary>
        QuerySystemLog = 262144,

        #endregion

        #region 用户组管理
        /// <summary>
        /// 查询按钮（用户组管理）
        /// </summary>
        QueryUserGroup = 524288,
        /// <summary>
        /// 新增按钮（用户组管理）
        /// </summary>
        AddUserGroup = 1048576,
        /// <summary>
        /// 修改按钮（用户组管理）
        /// </summary>
        UpdateUserGroup = 2097152,
        /// <summary>
        /// 删除按钮（用户组管理）
        /// </summary>
        DeleteUserGroup = 4194304,
        /// <summary>
        /// 设置角色按钮（用户组管理）
        /// </summary>
        SetRolesUserGroup = 8388608 

        #endregion
    }
}
