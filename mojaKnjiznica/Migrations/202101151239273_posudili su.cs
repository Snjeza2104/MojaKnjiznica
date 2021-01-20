namespace mojaKnjiznica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posudilisu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posidjivacs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(nullable: false, maxLength: 20),
                        Mobitel = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Posidjivacs");
        }
    }
}
