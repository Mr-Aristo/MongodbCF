using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repository.Abstract;
namespace DAL.Repository.MongoDb
{
    public class DbMongoRepo : IDbRepository
    {
        private MongoContext db;
        private OrderMongoRep orderRep;
        private ProductMongoRep productRep;
        private SuplierMongoRep suplierRep;

        public DbMongoRepo(string cs)
        {
            db = new MongoContext(cs);

            orderRep = new OrderMongoRep(db);
            productRep = new ProductMongoRep(db);
            suplierRep = new SuplierMongoRep(db);
        }

        public IRepository<Product> Product
        {
            get { return productRep; }
        }

        public IRepository<Order> Order
        {
            get { return orderRep; }
        }

        public IRepository<Suplier> Suplier
        {
            get { return suplierRep; }
        }

        public int Save()
        {
            return 1;
        }
    }
}
