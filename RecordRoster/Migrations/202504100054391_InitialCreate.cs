namespace RecordRoster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Artist = c.String(nullable: false),
                        ReleaseYear = c.Int(nullable: false),
                        Cover = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        AlbumId = c.Int(nullable: false),
                        TrackNumber = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.AlbumId, t.TrackNumber });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Songs");
            DropTable("dbo.Albums");
        }
    }
}
