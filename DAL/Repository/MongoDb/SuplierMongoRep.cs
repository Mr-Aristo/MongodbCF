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
    public class SuplierMongoRep:IRepository<Suplier>
    {
        private MongoContext db;

        public SuplierMongoRep(MongoContext context)
        {
            this.db = context;
        }

        public void Add(Suplier entity)
        {
            Suplier last = db.SuplierCollection.Find(new FilterDefinitionBuilder<Suplier>().Empty)
                .SortByDescending(i => i.ID).Limit(1).FirstOrDefault();
        }

        public List<Suplier> GetAll()
        {
            var builder = new FilterDefinitionBuilder<Suplier>();
            var filter = builder.Empty;
            return new List<Suplier>(db.SuplierCollection.Find(filter).ToListAsync().Result);
        }

        public Suplier GetByID(int id)
        {
            return db.SuplierCollection.Find(i => i.ID == id).FirstOrDefault();
        }

        public void Remove(int id)
        {
            db.SuplierCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id.ToString())));
        }

        public void Update(Suplier entity)
        {
            db.SuplierCollection.ReplaceOneAsync(new BsonDocument("_id", entity.ID), entity);
        }
    }
}
