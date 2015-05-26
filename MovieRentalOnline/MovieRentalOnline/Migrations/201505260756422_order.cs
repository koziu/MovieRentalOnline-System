namespace MovieRentalOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "ClientId" });
            AddColumn("dbo.Rents", "SingleCost", c => c.Double(nullable: false));
            AddColumn("dbo.Orders", "Client_ClientId", c => c.Int());
            AlterColumn("dbo.Orders", "ClientId", c => c.String());
            CreateIndex("dbo.Orders", "Client_ClientId");
            AddForeignKey("dbo.Orders", "Client_ClientId", "dbo.Clients", "ClientId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Client_ClientId", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "Client_ClientId" });
            AlterColumn("dbo.Orders", "ClientId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Client_ClientId");
            DropColumn("dbo.Rents", "SingleCost");
            CreateIndex("dbo.Orders", "ClientId");
            AddForeignKey("dbo.Orders", "ClientId", "dbo.Clients", "ClientId", cascadeDelete: true);
        }
    }
}
