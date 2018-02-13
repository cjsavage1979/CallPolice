namespace CallPolice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alarms",
                c => new
                    {
                        AlarmId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Longitide = c.String(),
                        Latitude = c.String(),
                        FileType = c.Int(nullable: false),
                        FileName = c.String(),
                        AlarmContent = c.String(),
                    })
                .PrimaryKey(t => t.AlarmId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alarms", "UserId", "dbo.Users");
            DropIndex("dbo.Alarms", new[] { "UserId" });
            DropTable("dbo.Alarms");
        }
    }
}
