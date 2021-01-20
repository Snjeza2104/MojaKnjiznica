namespace mojaKnjiznica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class knjiga2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Knjigas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naslov = c.String(nullable: false, maxLength: 100),
                        IdIzdavac = c.Int(nullable: false),
                        BrojStranica = c.Int(nullable: false),
                        Cijena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GodinaIzdanja = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Izdavacis", t => t.IdIzdavac, cascadeDelete: true)
                .Index(t => t.IdIzdavac);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Knjigas", "IdIzdavac", "dbo.Izdavacis");
            DropIndex("dbo.Knjigas", new[] { "IdIzdavac" });
            DropTable("dbo.Knjigas");
        }
    }
}
