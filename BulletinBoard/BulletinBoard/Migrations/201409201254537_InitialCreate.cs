namespace BulletinBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        IdAdvertisement = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        PublishDate = c.DateTime(nullable: false),
                        Contacts_IdContacts = c.Int(),
                    })
                .PrimaryKey(t => t.IdAdvertisement)
                .ForeignKey("dbo.Contacts", t => t.Contacts_IdContacts)
                .Index(t => t.Contacts_IdContacts);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        IdContacts = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.IdContacts);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertisements", "Contacts_IdContacts", "dbo.Contacts");
            DropIndex("dbo.Advertisements", new[] { "Contacts_IdContacts" });
            DropTable("dbo.Contacts");
            DropTable("dbo.Advertisements");
        }
    }
}
