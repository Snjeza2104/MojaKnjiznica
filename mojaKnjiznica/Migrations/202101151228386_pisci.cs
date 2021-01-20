namespace mojaKnjiznica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pisci : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Piscis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AutorId = c.Int(nullable: false),
                        KnjigaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Autoris", t => t.AutorId, cascadeDelete: true)
                .ForeignKey("dbo.Knjigas", t => t.KnjigaId, cascadeDelete: true)
                .Index(t => t.AutorId)
                .Index(t => t.KnjigaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Piscis", "KnjigaId", "dbo.Knjigas");
            DropForeignKey("dbo.Piscis", "AutorId", "dbo.Autoris");
            DropIndex("dbo.Piscis", new[] { "KnjigaId" });
            DropIndex("dbo.Piscis", new[] { "AutorId" });
            DropTable("dbo.Piscis");
        }
    }
}
