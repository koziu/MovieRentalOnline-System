namespace MovieRentalOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        ActorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Biography = c.String(),
                        PhotoFileName = c.String(),
                    })
                .PrimaryKey(t => t.ActorId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        PhotoFileName = c.String(),
                    })
                .PrimaryKey(t => t.MovieId);
            
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        DirectorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Biography = c.String(),
                        PhotoFileName = c.String(),
                    })
                .PrimaryKey(t => t.DirectorId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                        IconFileName = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        Cost = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LanguageType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LanguageId);
            
            CreateTable(
                "dbo.PhysicalProducts",
                c => new
                    {
                        PhysicalProductId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Description = c.String(),
                        IsHidden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PhysicalProductId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Rents",
                c => new
                    {
                        RentId = c.Int(nullable: false, identity: true),
                        PhysicalProductId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        RentStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RentId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.PhysicalProducts", t => t.PhysicalProductId, cascadeDelete: true)
                .Index(t => t.PhysicalProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        AdditionalInformation = c.Int(nullable: false),
                        OrderStatus = c.Int(nullable: false),
                        DeliveryTime = c.DateTime(nullable: false),
                        ReturnTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.SoundTechnologies",
                c => new
                    {
                        SoundTechnologyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SoundTechnologyId);
            
            CreateTable(
                "dbo.StorageMediums",
                c => new
                    {
                        StorageMediumId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.StorageMediumId);
            
            CreateTable(
                "dbo.VideoTechnologies",
                c => new
                    {
                        VideoTechnologyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.VideoTechnologyId);
            
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        Movie_MovieId = c.Int(nullable: false),
                        Actor_ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_MovieId, t.Actor_ActorId })
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actor_ActorId, cascadeDelete: true)
                .Index(t => t.Movie_MovieId)
                .Index(t => t.Actor_ActorId);
            
            CreateTable(
                "dbo.DirectorMovies",
                c => new
                    {
                        Director_DirectorId = c.Int(nullable: false),
                        Movie_MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Director_DirectorId, t.Movie_MovieId })
                .ForeignKey("dbo.Directors", t => t.Director_DirectorId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .Index(t => t.Director_DirectorId)
                .Index(t => t.Movie_MovieId);
            
            CreateTable(
                "dbo.GenreMovies",
                c => new
                    {
                        Genre_GenreId = c.Int(nullable: false),
                        Movie_MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreId, t.Movie_MovieId })
                .ForeignKey("dbo.Genres", t => t.Genre_GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .Index(t => t.Genre_GenreId)
                .Index(t => t.Movie_MovieId);
            
            CreateTable(
                "dbo.LanguageProducts",
                c => new
                    {
                        Language_LanguageId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_LanguageId, t.Product_ProductId })
                .ForeignKey("dbo.Languages", t => t.Language_LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .Index(t => t.Language_LanguageId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.SoundTechnologyProducts",
                c => new
                    {
                        SoundTechnology_SoundTechnologyId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SoundTechnology_SoundTechnologyId, t.Product_ProductId })
                .ForeignKey("dbo.SoundTechnologies", t => t.SoundTechnology_SoundTechnologyId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .Index(t => t.SoundTechnology_SoundTechnologyId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.StorageMediumProducts",
                c => new
                    {
                        StorageMedium_StorageMediumId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StorageMedium_StorageMediumId, t.Product_ProductId })
                .ForeignKey("dbo.StorageMediums", t => t.StorageMedium_StorageMediumId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .Index(t => t.StorageMedium_StorageMediumId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.VideoTechnologyProducts",
                c => new
                    {
                        VideoTechnology_VideoTechnologyId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VideoTechnology_VideoTechnologyId, t.Product_ProductId })
                .ForeignKey("dbo.VideoTechnologies", t => t.VideoTechnology_VideoTechnologyId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .Index(t => t.VideoTechnology_VideoTechnologyId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoTechnologyProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.VideoTechnologyProducts", "VideoTechnology_VideoTechnologyId", "dbo.VideoTechnologies");
            DropForeignKey("dbo.StorageMediumProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.StorageMediumProducts", "StorageMedium_StorageMediumId", "dbo.StorageMediums");
            DropForeignKey("dbo.SoundTechnologyProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.SoundTechnologyProducts", "SoundTechnology_SoundTechnologyId", "dbo.SoundTechnologies");
            DropForeignKey("dbo.Rents", "PhysicalProductId", "dbo.PhysicalProducts");
            DropForeignKey("dbo.Rents", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.PhysicalProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.LanguageProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.LanguageProducts", "Language_LanguageId", "dbo.Languages");
            DropForeignKey("dbo.GenreMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Genre_GenreId", "dbo.Genres");
            DropForeignKey("dbo.DirectorMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.DirectorMovies", "Director_DirectorId", "dbo.Directors");
            DropForeignKey("dbo.MovieActors", "Actor_ActorId", "dbo.Actors");
            DropForeignKey("dbo.MovieActors", "Movie_MovieId", "dbo.Movies");
            DropIndex("dbo.VideoTechnologyProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.VideoTechnologyProducts", new[] { "VideoTechnology_VideoTechnologyId" });
            DropIndex("dbo.StorageMediumProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.StorageMediumProducts", new[] { "StorageMedium_StorageMediumId" });
            DropIndex("dbo.SoundTechnologyProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.SoundTechnologyProducts", new[] { "SoundTechnology_SoundTechnologyId" });
            DropIndex("dbo.LanguageProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.LanguageProducts", new[] { "Language_LanguageId" });
            DropIndex("dbo.GenreMovies", new[] { "Movie_MovieId" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_GenreId" });
            DropIndex("dbo.DirectorMovies", new[] { "Movie_MovieId" });
            DropIndex("dbo.DirectorMovies", new[] { "Director_DirectorId" });
            DropIndex("dbo.MovieActors", new[] { "Actor_ActorId" });
            DropIndex("dbo.MovieActors", new[] { "Movie_MovieId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropIndex("dbo.Rents", new[] { "OrderId" });
            DropIndex("dbo.Rents", new[] { "PhysicalProductId" });
            DropIndex("dbo.PhysicalProducts", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "MovieId" });
            DropTable("dbo.VideoTechnologyProducts");
            DropTable("dbo.StorageMediumProducts");
            DropTable("dbo.SoundTechnologyProducts");
            DropTable("dbo.LanguageProducts");
            DropTable("dbo.GenreMovies");
            DropTable("dbo.DirectorMovies");
            DropTable("dbo.MovieActors");
            DropTable("dbo.VideoTechnologies");
            DropTable("dbo.StorageMediums");
            DropTable("dbo.SoundTechnologies");
            DropTable("dbo.Clients");
            DropTable("dbo.Orders");
            DropTable("dbo.Rents");
            DropTable("dbo.PhysicalProducts");
            DropTable("dbo.Languages");
            DropTable("dbo.Products");
            DropTable("dbo.Genres");
            DropTable("dbo.Directors");
            DropTable("dbo.Movies");
            DropTable("dbo.Actors");
        }
    }
}
