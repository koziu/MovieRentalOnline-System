namespace MovieRentalOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adres2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserAddresses", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAddresses", "UserId", c => c.Int(nullable: false));
        }
    }
}
