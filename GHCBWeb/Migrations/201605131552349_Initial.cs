namespace GHCBWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserBoards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BoardId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        ApplicationUserName = c.String(),
                        UserType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Boards", t => t.BoardId, cascadeDelete: true)
                .Index(t => t.BoardId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Text1 = c.String(),
                        UserId = c.String(nullable: false, maxLength: 100),
                        UserToken = c.String(nullable: false, maxLength: 100),
                        Level = c.Byte(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNo = c.String(),
                        Name = c.String(),
                        MAC = c.String(),
                        ROMVersion = c.String(),
                        Description = c.String(),
                        Privateip = c.String(),
                        Publicip = c.String(),
                        SSID = c.String(),
                        BSSID = c.String(),
                        Token = c.String(),
                        Deviceid = c.String(),
                        Status = c.String(),
                        Offtime = c.DateTime(),
                        Onlinetime = c.DateTime(),
                        Createtime = c.DateTime(),
                        Board_Id = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.Board_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Board_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.BoardPorts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BoardId = c.Int(nullable: false),
                        Port = c.Int(nullable: false),
                        Alias = c.String(),
                        PortDescriptionId = c.Int(nullable: false),
                        Control = c.String(),
                        X = c.Double(),
                        Y = c.Double(),
                        Width = c.Double(),
                        Length = c.Double(),
                        Color = c.String(),
                        Enable = c.String(),
                        Uplimit = c.Double(),
                        Lowlimit = c.Double(),
                        Max = c.Double(),
                        Min = c.Double(),
                        DataType = c.String(),
                        DefaultValue = c.Double(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PortDescriptions", t => t.PortDescriptionId, cascadeDelete: true)
                .ForeignKey("dbo.Boards", t => t.BoardId, cascadeDelete: true)
                .Index(t => t.BoardId)
                .Index(t => t.PortDescriptionId);
            
            CreateTable(
                "dbo.PortDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceModel = c.String(),
                        DeviceType = c.String(),
                        Direction = c.String(),
                        Description = c.String(),
                        ClassType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Apps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AppId = c.String(),
                        AppSecretKey = c.String(),
                        SMSServer = c.String(),
                        Text1 = c.String(),
                        Text2 = c.String(),
                        Text3 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ApplicationUserBoards", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.ApplicationUserBoards", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Boards", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BoardPorts", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.BoardPorts", "PortDescriptionId", "dbo.PortDescriptions");
            DropForeignKey("dbo.Boards", "Board_Id", "dbo.Boards");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.BoardPorts", new[] { "PortDescriptionId" });
            DropIndex("dbo.BoardPorts", new[] { "BoardId" });
            DropIndex("dbo.Boards", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Boards", new[] { "Board_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ApplicationUserBoards", new[] { "ApplicationUserId" });
            DropIndex("dbo.ApplicationUserBoards", new[] { "BoardId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Apps");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.PortDescriptions");
            DropTable("dbo.BoardPorts");
            DropTable("dbo.Boards");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ApplicationUserBoards");
        }
    }
}
