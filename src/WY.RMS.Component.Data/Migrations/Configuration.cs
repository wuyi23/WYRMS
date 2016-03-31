using WY.RMS.Component.Data.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using WY.RMS.Component.Data.Enum;
using WY.RMS.Domain.Model.Member;

namespace WY.RMS.Component.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EFDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            #region 模块管理
            List<Module> modules = new List<Module>
            {
                new Module { Id = 1, ParentId = null, Name = "授权管理", Code = 200,LinkUrl="#",  Description = null, IsMenu = true, Enabled = true,  UpdateDate = DateTime.Now},
                new Module { Id = 2, ParentId = 1, Name = "角色管理", LinkUrl = "~/Member/Role/Index",  Code = 201,Description = null, IsMenu = true, Enabled = true, UpdateDate = DateTime.Now},
                new Module { Id = 3, ParentId = 1, Name = "用户管理", LinkUrl = "~/Member/User/Index", Code = 202, Description = null, IsMenu = true, Enabled = true, UpdateDate = DateTime.Now },
                new Module { Id = 4, ParentId = 1, Name = "模块管理", LinkUrl = "~/Member/Module/Index",  Code = 204, Description = null, IsMenu = true, Enabled = true,  UpdateDate = DateTime.Now },
                new Module { Id = 5, ParentId = 1, Name = "权限管理", LinkUrl = "~/Member/Permission/Index",  Code = 205, Description = null, IsMenu = true, Enabled = true,  UpdateDate = DateTime.Now },
                 new Module { Id = 6, ParentId = null, Name = "系统应用", LinkUrl = "#",  Code = 300,  Description = null, IsMenu = true, Enabled = true,  UpdateDate = DateTime.Now },
                new Module { Id = 7, ParentId = 6, Name = "操作日志管理", LinkUrl = "#",Code = 301,Description = null, IsMenu = true, Enabled = true,  UpdateDate = DateTime.Now },
                new Module { Id = 8, ParentId = 1, Name = "用户组管理", LinkUrl = "~/Member/UserGroup/Index",  Code = 203, Description = null, IsMenu = true, Enabled = true,  UpdateDate = DateTime.Now }
                //~/SysConfig/OperateLog/Index
            };
            DbSet<Module> moduleSet = context.Set<Module>();
            moduleSet.AddOrUpdate(t => new { t.Name }, modules.ToArray());
            context.SaveChanges();
            #endregion

            #region 权限管理
            List<Permission> permissions = new List<Permission>
            {
             #region 角色
		       new Permission{Id=1, Name="查询",Code=EnumPermissionCode.QueryRole.ToString(), 
                    Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[1]},
               new Permission{Id=2, Name="新增",Code=EnumPermissionCode.AddRole.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[1]},
               new Permission{Id=3, Name="修改",Code=EnumPermissionCode.UpdateRole.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[1]},
               new Permission{Id=4, Name="删除",Code=EnumPermissionCode.DeleteRole.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[1]},
               new Permission{Id=5, Name="授权",Code=EnumPermissionCode.AuthorizeRole.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[1]}, 
             #endregion

             #region 用户
		       new Permission{Id=6, Name="查询",Code=EnumPermissionCode.QueryUser.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[2]},
               new Permission{Id=7, Name="新增",Code=EnumPermissionCode.AddUser.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[2]},
               new Permission{Id=8, Name="修改",Code=EnumPermissionCode.UpdateUser.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[2]},
               new Permission{Id=9, Name="删除",Code=EnumPermissionCode.DeleteUser.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[2]},
               new Permission{Id=10, Name="重置密码",Code=EnumPermissionCode.ResetPwdUser.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[2]},
               new Permission{Id=11, Name="设置用户组",Code=EnumPermissionCode.SetGroupUser.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[2]},
               new Permission{Id=12, Name="设置角色",Code=EnumPermissionCode.SetRolesUser.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[2]}, 
	         #endregion
             
             #region 模块
		     new Permission{Id=13, Name="查询",Code=EnumPermissionCode.QueryModule.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[3]},
             new Permission{Id=14, Name="新增",Code=EnumPermissionCode.AddModule.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[3]},
             new Permission{Id=15, Name="修改",Code=EnumPermissionCode.UpdateModule.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[3]},
	         #endregion

             #region 权限
		     new Permission{Id=16, Name="查询",Code=EnumPermissionCode.QueryPermission.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[4]},
             new Permission{Id=17, Name="新增",Code=EnumPermissionCode.AddPermission.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[4]},
             new Permission{Id=18, Name="修改",Code=EnumPermissionCode.UpdatePermission.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[4]},
	         #endregion

             #region 操作日志
		     new Permission{Id=19, Name="查询",Code=EnumPermissionCode.QuerySystemLog.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[6]},
	         #endregion

             #region 用户组
		     new Permission{Id=20, Name="查询",Code=EnumPermissionCode.QueryUserGroup.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[7]},
             new Permission{Id=21, Name="新增",Code=EnumPermissionCode.AddUserGroup.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[7]},
             new Permission{Id=22, Name="修改",Code=EnumPermissionCode.UpdateUserGroup.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[7]},
             new Permission{Id=23, Name="删除",Code=EnumPermissionCode.DeleteUserGroup.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[7]},
             new Permission{Id=24, Name="设置角色",Code=EnumPermissionCode.SetRolesUserGroup.ToString(), Description="描述" ,Enabled=true,UpdateDate=DateTime.Now,module=modules[7]}
	         #endregion
            };
            DbSet<Permission> permissionSet = context.Set<Permission>();
            permissionSet.AddOrUpdate(m => new { m.Id }, permissionSet.ToArray());
            context.SaveChanges();
            #endregion

            #region 角色管理
            List<Role> roles = new List<Role>
            {
                new Role { Id=1,  RoleName = "superadmin", Description="超级管理员",Enabled=true,OrderSort=1,UpdateDate=DateTime.Now ,Permissions=permissions},
                new Role { Id=2,  RoleName = "管理员", Description="系统管理员",Enabled=true,OrderSort=1,UpdateDate=DateTime.Now,Permissions=permissions},
                new Role { Id=3,  RoleName = "普通角色1", Description="普通角色1",Enabled=true,OrderSort=1,UpdateDate=DateTime.Now ,Permissions=permissions},
                new Role { Id=4,  RoleName = "普通角色2", Description="普通角色2",Enabled=true,OrderSort=1,UpdateDate=DateTime.Now },
                new Role { Id=5,  RoleName = "普通角色3", Description="普通角色3",Enabled=true,OrderSort=1,UpdateDate=DateTime.Now },
                new Role { Id=6,  RoleName = "普通角色4", Description="普通角色4",Enabled=true,OrderSort=1,UpdateDate=DateTime.Now },
                new Role { Id=7,  RoleName = "普通角色5", Description="普通角色5",Enabled=true,OrderSort=1,UpdateDate=DateTime.Now },
                new Role { Id=8,  RoleName = "普通角色6", Description="普通角色6",Enabled=true,OrderSort=1,UpdateDate=DateTime.Now },
                new Role { Id=9,  RoleName = "普通角色7", Description="普通角色7",Enabled=true,OrderSort=1,UpdateDate=DateTime.Now },
                new Role { Id=10,  RoleName = "普通角色8", Description="普通角色8",Enabled=true,OrderSort=1,UpdateDate=DateTime.Now },
                new Role { Id=11,  RoleName = "普通角色9", Description="普通角色9",Enabled=true,OrderSort=1,UpdateDate=DateTime.Now },
                new Role { Id=12,  RoleName = "普通角色10", Description="普通角色10",Enabled=true,OrderSort=1,UpdateDate=DateTime.Now }
            };
            DbSet<Role> roleSet = context.Set<Role>();
            roleSet.AddOrUpdate(m => new { m.RoleName }, roles.ToArray());
            context.SaveChanges();
            #endregion

            #region 用户管理
            List<User> members = new List<User>
            {
                new User { Id=1, UserName = "admin", Password = "e10adc3949ba59abbe56e057f20f883e", Email = "375368093@qq.com", TrueName = "管理员",Phone="18181818181",Address="广东广州市天河区科韵路XX街XX号XXX房X号" ,Enabled=true,Roles=new List<Role>{roles[1]} },
                new User { Id=2, UserName = "xiaowu", Password = "e10adc3949ba59abbe56e057f20f883e", Email = "11111@1111.com", TrueName = "小吴",Phone="18181818181",Address="广东广州市天河区科韵路XX街X广东广州市天河区科韵路XX街XX号XXX房X号",Enabled=true,Roles=new List<Role>{roles[1]} }
            };
            DbSet<User> memberSet = context.Set<User>();
            memberSet.AddOrUpdate(m => new { m.UserName }, members.ToArray());
            context.SaveChanges();
            #endregion

            #region 用户组管理
            List<UserGroup> userGroups = new List<UserGroup>
            {
                new UserGroup { Id=1, GroupName = "开发组",Description = "开发人员组",Enabled=true,Roles=new List<Role>{roles[1]},OrderSort = 1,Users = new List<User>(){members[0]}},
                new UserGroup { Id=2, GroupName = "项目经理组", Description = "项目经理组",Enabled=true,Roles=new List<Role>{roles[1]},OrderSort = 2,Users = new List<User>(){members[1]}}
            };
            DbSet<UserGroup> userGroupsSet = context.Set<UserGroup>();
            userGroupsSet.AddOrUpdate(m => new { m.GroupName }, userGroups.ToArray());
            context.SaveChanges();
            #endregion
        }
    }
}
