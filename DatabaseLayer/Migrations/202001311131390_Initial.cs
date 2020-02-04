namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressDB",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        DeliveryAddress = c.String(),
                    })
                .PrimaryKey(t => t.AddressID);
            
            CreateTable(
                "dbo.ProductDB",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ModelNumber = c.String(),
                        Price = c.Double(nullable: false),
                        Description = c.String(),
                        DeliveryTime = c.Int(nullable: false),
                        AvailableQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.RegisterDB",
                c => new
                    {
                        RegisterID = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RegisterID);
            
            CreateTable(
                "dbo.UserProductDBModel",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        ModelNumber = c.String(),
                        RequiredQuantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Description = c.String(),
                        DeliveryTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProductDBModel");
            DropTable("dbo.RegisterDB");
            DropTable("dbo.ProductDB");
            DropTable("dbo.AddressDB");
        }
    }
}
