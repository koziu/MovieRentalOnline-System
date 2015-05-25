namespace MovieRentalOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adres4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAddresses", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAddresses", "Phone");
        }
    }
}
