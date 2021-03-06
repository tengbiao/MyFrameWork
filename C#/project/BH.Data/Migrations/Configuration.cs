namespace BH.Data.Migrations
{
    using BH.Code;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BH.Data.BHDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BH.Data.BHDbContext context)
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

            string adminId = "9f2ec079-7d0f-4fe2-90ab-8b09a8302aba";

            context.Sys_User.AddOrUpdate(p => p.F_Id,
                new Domain.Entity.Sys_User
                {
                    F_Id = adminId,
                    F_Account = "admin",
                    F_RealName = "超级管理员",
                    F_NickName = "超级管理员",
                    F_EnabledMark = true,
                    F_DeleteMark = false,
                    F_CreatorTime = DateTime.Now,
                    F_CreatorUserId = "auto"
                });
            string secretKey = Encryptor.Md5Encryptor16(Common.CreateNo()).ToLower();
            context.Sys_UserLogOn.AddOrUpdate(p => p.F_Id,
                new Domain.Entity.Sys_UserLogOn()
                {
                    F_Id = adminId,
                    F_UserId = adminId,
                    F_UserPassword = Encryptor.Md5Encryptor32(Encryptor.DesEncrypt(Encryptor.Md5Encryptor32("0000").ToLower(), secretKey).ToLower()).ToLower(),
                    F_UserSecretkey = secretKey
                });
        }
    }
}
