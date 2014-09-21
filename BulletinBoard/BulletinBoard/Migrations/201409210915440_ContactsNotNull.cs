namespace BulletinBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactsNotNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Advertisements", "Contacts_IdContacts", "dbo.Contacts");
            DropIndex("dbo.Advertisements", new[] { "Contacts_IdContacts" });
            AlterColumn("dbo.Advertisements", "Contacts_IdContacts", c => c.Int(nullable: false));
            CreateIndex("dbo.Advertisements", "Contacts_IdContacts");
            AddForeignKey("dbo.Advertisements", "Contacts_IdContacts", "dbo.Contacts", "IdContacts", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertisements", "Contacts_IdContacts", "dbo.Contacts");
            DropIndex("dbo.Advertisements", new[] { "Contacts_IdContacts" });
            AlterColumn("dbo.Advertisements", "Contacts_IdContacts", c => c.Int());
            CreateIndex("dbo.Advertisements", "Contacts_IdContacts");
            AddForeignKey("dbo.Advertisements", "Contacts_IdContacts", "dbo.Contacts", "IdContacts");
        }
    }
}
