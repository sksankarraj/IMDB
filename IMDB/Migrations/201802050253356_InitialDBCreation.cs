namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDBCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        DOB = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ActorMovie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ActorID = c.Int(nullable: false),
                        MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Actor", t => t.ActorID, cascadeDelete: true)
                .ForeignKey("dbo.Movie", t => t.MovieID, cascadeDelete: true)
                .Index(t => t.ActorID)
                .Index(t => t.MovieID);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Plot = c.String(maxLength: 500),
                        Poster = c.String(maxLength: 50),
                        ProducerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Producer", t => t.ProducerID, cascadeDelete: true)
                .Index(t => t.ProducerID);
            
            CreateTable(
                "dbo.Producer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        DOB = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movie", "ProducerID", "dbo.Producer");
            DropForeignKey("dbo.ActorMovie", "MovieID", "dbo.Movie");
            DropForeignKey("dbo.ActorMovie", "ActorID", "dbo.Actor");
            DropIndex("dbo.Movie", new[] { "ProducerID" });
            DropIndex("dbo.ActorMovie", new[] { "MovieID" });
            DropIndex("dbo.ActorMovie", new[] { "ActorID" });
            DropTable("dbo.Producer");
            DropTable("dbo.Movie");
            DropTable("dbo.ActorMovie");
            DropTable("dbo.Actor");
        }
    }
}
