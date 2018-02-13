namespace CallPolice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PoliceNews",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        NewsTitle = c.String(),
                        NewsContent = c.String(),
                    })
                .PrimaryKey(t => t.NewsId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserCellPhone = c.String(nullable: false),
                        UserPwd = c.String(nullable: false),
                        UserAddress = c.String(),
                        UserRelative = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.PoliceNews");
        }
    }
}
