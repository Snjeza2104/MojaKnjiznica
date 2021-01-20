namespace mojaKnjiznica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ps : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PosudiliSus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PosidjivacId = c.Int(nullable: false),
                        KnjigaId = c.Int(nullable: false),
                        DatumPosudbe = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Knjigas", t => t.KnjigaId, cascadeDelete: true)
                .ForeignKey("dbo.Posidjivacs", t => t.PosidjivacId, cascadeDelete: true)
                .Index(t => t.PosidjivacId)
                .Index(t => t.KnjigaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PosudiliSus", "PosidjivacId", "dbo.Posidjivacs");
            DropForeignKey("dbo.PosudiliSus", "KnjigaId", "dbo.Knjigas");
            DropIndex("dbo.PosudiliSus", new[] { "KnjigaId" });
            DropIndex("dbo.PosudiliSus", new[] { "PosidjivacId" });
            DropTable("dbo.PosudiliSus");
        }
    }
}
