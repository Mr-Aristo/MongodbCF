using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
namespace DAL
{
   public  class MongoContext
    {
        string Constring;
        
        IMongoDatabase database;


        public IMongoCollection<Order> OrderCollection
        {
            get { return database.GetCollection<Order>("Order"); }
        }

        public IMongoCollection<Product> ProductCollection
        {
            get { return database.GetCollection<Product>("Product"); }
        }

        public IMongoCollection<Suplier> SuplierCollection
        {
            get { return database.GetCollection<Suplier>("Suplier"); }
        }

        public MongoContext(string cs)
        {
            Constring = cs;
            var connetion = new MongoUrlBuilder(Constring);
            MongoClient client = new MongoClient(Constring);
            database = client.GetDatabase(connetion.DatabaseName);
        }
    }
}
