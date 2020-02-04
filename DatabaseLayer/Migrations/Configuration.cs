namespace DatabaseLayer.Migrations
{
    using DatabaseLayer.ModelsDB;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseLayer.Context.ECommerceDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseLayer.Context.ECommerceDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var users = new List<RegisterDB>()
            {
                new RegisterDB() { RegisterID=1, FullName = "Ravi Agarwal", Email = "ravi@gmail.com", Password = "ravi@123", IsAdmin = true },
                new RegisterDB() { RegisterID=2, FullName = "Lovin Singla", Email = "lovin@gmail.com", Password = "lovin@123", IsAdmin = false },
                new RegisterDB() { RegisterID=3, FullName = "Shubham", Email = "shubham@gmail.com", Password = "shubham@123", IsAdmin = false }
            };
            users.ForEach(x => context.registerDB.AddOrUpdate(x));
            context.SaveChanges();

            var products = new List<ProductDB>()
            {
                new ProductDB() { ProductID=1, ModelNumber = "#1", Description = "Bulb", AvailableQuantity = 10, Price = 5, DeliveryTime = 2 },
                new ProductDB() { ProductID=2, ModelNumber = "#2", Description = "LED", AvailableQuantity = 15, Price = 23, DeliveryTime = 3 },
                new ProductDB() { ProductID=3, ModelNumber = "#3", Description = "Lamp", AvailableQuantity = 20, Price = 12, DeliveryTime = 5 }
            };
            products.ForEach(x => context.productDB.AddOrUpdate(x));
            context.SaveChanges();

            var deliveryAddress = new List<AddressDB>()
            {
                new AddressDB() { AddressID=1, DeliveryAddress="Ravi Address", Email="ravi@gmail.com" },
                new AddressDB() { AddressID=2, DeliveryAddress="Lovin Address", Email="lovin@gmail.com" },
                new AddressDB() { AddressID=3, DeliveryAddress="Shubham Address", Email="shubham@gmail.com" },
            };
            deliveryAddress.ForEach(x => context.addressDB.AddOrUpdate(x));
            context.SaveChanges();

            var productList = new List<UserProductDBModel>()
            {
                new UserProductDBModel(){ UserID = 1, Email = "ravi@gmail.com", ModelNumber = "#1", RequiredQuantity = 4, Price = 2, Description = "Bulb", DeliveryTime = 2},
                new UserProductDBModel(){ UserID = 2, Email = "ravi@gmail.com", ModelNumber = "#2", RequiredQuantity = 2, Price = 5, Description = "Lamp", DeliveryTime = 3},
                new UserProductDBModel(){ UserID = 3, Email = "ravi@gmail.com", ModelNumber = "#3", RequiredQuantity = 7, Price = 10, Description = "TubeLight", DeliveryTime = 5}
            };
            productList.ForEach(x => context.userProductDBModels.AddOrUpdate(x));
            context.SaveChanges();
        }
    }
}
