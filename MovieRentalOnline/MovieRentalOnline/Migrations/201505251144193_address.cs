namespace MovieRentalOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class address : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        UserAddressId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                        Street = c.String(),
                        HomeNo = c.String(),
                        Postal = c.String(),
                        CityPostal = c.String(),
                        City = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.UserAddressId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserAddresses");
        }
    }
}
