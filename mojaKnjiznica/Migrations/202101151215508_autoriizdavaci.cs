namespace mojaKnjiznica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autoriizdavaci : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Izdavacis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 25),
                        Adresa = c.String(maxLength: 100),
                        Mobitel = c.String(maxLength: 20),
                        Email = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Izdavacis");
        }
    }
}
