namespace IMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2ndUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actor", "Sex", c => c.String(nullable: false, maxLength: 1));
            AddColumn("dbo.Producer", "Sex", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.Actor", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Movie", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Movie", "Plot", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Movie", "Poster", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Producer", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Producer", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Movie", "Poster", c => c.String(maxLength: 50));
            AlterColumn("dbo.Movie", "Plot", c => c.String(maxLength: 500));
            AlterColumn("dbo.Movie", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.Actor", "Name", c => c.String(maxLength: 50));
            DropColumn("dbo.Producer", "Sex");
            DropColumn("dbo.Actor", "Sex");
        }
    }
}
