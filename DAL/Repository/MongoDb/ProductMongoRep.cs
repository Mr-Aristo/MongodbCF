using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL.Repository.MongoDb
{
   public class ProductMongoRep:IRepository<Product>
    {
        private MongoContext db;

        public ProductMongoRep(MongoContext context)
        {
            this.db = context;
        }

        public void Add(Product entity)
        {
            Product last = db.ProductCollection.Find(new FilterDefinitionBuilder<Product>().Empty)
                .SortByDescending(i => i.ID).Limit(1).FirstOrDefault();
            entity.ID = last != null ? last.ID + 1 : 1;
            db.ProductCollection.InsertOneAsync(entity).Wait();
        }



        public List<Product> GetAll()
        {

            var builder = new FilterDefinitionBuilder<Product>();
            var filter = builder.Empty;
            return new List<Product>(db.ProductCollection.Find(filter).ToListAsync().Result);
        }

        public Product GetByID(int id)
        {
            return db.ProductCollection.Find(i => i.ID == id).FirstOrDefault();
        }

        public void Remove(int id)
        {
            db.ProductCollection.DeleteManyAsync(new BsonDocument("_id", new ObjectId(id.ToString())));
        }

        public void Update(Product entity)
        {
            db.ProductCollection.ReplaceOneAsync(new BsonDocument("_id", entity.ID), entity);
        }
    }
}
