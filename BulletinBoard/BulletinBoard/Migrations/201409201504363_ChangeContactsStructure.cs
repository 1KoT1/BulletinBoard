namespace BulletinBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeContactsStructure : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Contacts", "Address", "Text");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Contacts", "Text", "Address");
        }
    }
}
