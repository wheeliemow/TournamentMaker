namespace TournamentReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        Address = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Games", "FieldId", c => c.Int());
            CreateIndex("dbo.Games", "FieldId");
            AddForeignKey("dbo.Games", "FieldId", "dbo.Fields", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "FieldId", "dbo.Fields");
            DropIndex("dbo.Games", new[] { "FieldId" });
            DropColumn("dbo.Games", "FieldId");
            DropTable("dbo.Fields");
        }
    }
}
