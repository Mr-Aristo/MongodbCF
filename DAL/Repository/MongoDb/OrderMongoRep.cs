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
    public class OrderMongoRep:IRepository<Order>
    {
        private MongoContext db;

        public OrderMongoRep(MongoContext context)
        {
           this.db = context;
        }

        public void Add(Order entity)
        {
            Order last = db.OrderCollection.Find(new FilterDefinitionBuilder<Order>().Empty)
                .SortByDescending(i => i.ID).Limit(1).FirstOrDefault();
            entity.ID = last != null ? last.ID + 1 : 1;
            db.OrderCollection.InsertOneAsync(entity).Wait();
        }

        public List<Order> GetAll()
        {
            var builder = new FilterDefinitionBuilder<Order>();
            var filter = builder.Empty;
            return new List<Order>(db.OrderCollection.Find(filter).ToListAsync().Result);
        }

        public Order GetByID(int id)
        {
            return db.OrderCollection.Find(i => i.ID == id).FirstOrDefault();
        }

        public void Remove(int id)
        {
            db.OrderCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id.ToString())));
        }
        public void Update(Order entity) 
        {
            db.OrderCollection.ReplaceOneAsync(new BsonDocument("_id", entity.ID),entity);
        }
    }
    
}
