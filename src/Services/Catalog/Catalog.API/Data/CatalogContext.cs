using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            MongoClient dbClient = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var dataBase = dbClient.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = dataBase.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
