namespace GHCBWeb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GHCBWeb.Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Data.Entities;
    internal sealed class Configuration : DbMigrationsConfiguration<GHCBWeb.Infrastructure.ApplicationDbContext>

    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GHCBWeb.Infrastructure.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.PortDescriptions.AddOrUpdate(
              p => p.DeviceType,
                     new PortDescription { DeviceModel = "rly", DeviceType = "PA1A-5V", Direction = "OUT", Description = "继电器", ClassType = "com.hhnext.myeasylink.RlyPort" },
                     new PortDescription { DeviceModel = "rly", DeviceType = "PA1A-12V", Direction = "OUT", Description = "继电器", ClassType = "com.hhnext.myeasylink.RlyPort" },
                     new PortDescription { DeviceModel = "temp", DeviceType = "DHT11", Direction = "IN", Description = "温湿度传感器", ClassType = "com.hhnext.myeasylink.TempPort" },
                     new PortDescription { DeviceModel = "temp", DeviceType = "AM2301", Direction = "IN", Description = "温湿度传感器", ClassType = "com.hhnext.myeasylink.TempPort" },
                     new PortDescription { DeviceModel = "camera", DeviceType = "PTC08A", Direction = "IN", Description = "串口摄像头", ClassType = "com.hhnext.myeasylink.Camera" }
            );

            context.Apps.AddOrUpdate(
                p => p.Name,
                 new App { Name = "app", AppId = "6a3d6800-1b07-4fc5-86ca-12bba8f8dc67", AppSecretKey = "7c734a44ed8450aff7f8fb7f958e7d90" },
                 new App { Name = "product", AppId = "2f10b621", AppSecretKey = "9a740523-d5f3-40f3-afb4-29b6d7c2726a" },
                 new App { Name = "qiniu", AppId = "CPrn8bw4AyovxztgH88EZc7Q7Nf6Zfb0140gzgEh", AppSecretKey = "rxJYJMNUPyx1CnWs87Yl2o26VPceJEi4ot-9Bq06" }


                );

            //context.Boards.AddOrUpdate(
            // p => p.SerialNo,
            //        new Board
            //        {
            //            SerialNo = "001",
            //            Alias = "木耳大棚1",
            //            MAC = "0027110939820",
            //            ROMVersion = "v103",
            //            Description = "3165",
            //            Privateip = "192.168.1.21",
            //            Publicip = "10.10.10.1",
            //            SSID = "x201_network",
            //            BSSID = "CEA2711A939EB1",
            //            Token = "",
            //            Deviceid = "",
            //            Status = "offline",
            //            Offtime=DateTime.Now,
            //            Onlinetime= DateTime.Now,
            //            Createtime = DateTime.Now
            //        },
            //        new Board
            //        {
            //            SerialNo = "002",
            //            Alias = "木耳大棚2",
            //            MAC = "0027110939821",
            //            ROMVersion = "v103",
            //            Description = "3165",
            //            Privateip = "192.168.1.22",
            //            Publicip = "10.10.10.2",
            //            SSID = "x201_network",
            //            BSSID = "CEA2711A939EB2",
            //            Token = "",
            //            Deviceid = "",
            //            Status = "offline",
            //             Offtime = DateTime.Now,
            //            Onlinetime = DateTime.Now,
            //            Createtime = DateTime.Now
            //        });

            //context.BoardPorts.AddOrUpdate(
            //p => p.BoardId,
            //       new BoardPort
            //       {
            //           BoardId = 1,
            //           Port = 1,
            //           PortDescriptionId = 1,
            //           Alias = "水泵1"
            //       },
            //       new BoardPort
            //       {

            //           BoardId = 1,
            //           Port = 2,
            //           PortDescriptionId = 3,
            //           Alias = "大棚东温度"
            //       });

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "13701308059@vip.163.com",
                Email = "1@2.3",
                EmailConfirmed = true,
                UserToken = "13701308059",
                UserId = "13701308059",
                Level = 1,
                JoinDate = DateTime.Now.Date
            };

            manager.Create(user, "123456");

            if (roleManager.Roles.Count() == 0)
            {
                //roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("13701308059@vip.163.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin" });

        }
    }
}
