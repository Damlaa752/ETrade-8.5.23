using ETrade.Business.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Concrete
{
    public class Repository<Tcontext, Tentity> : IRepository<Tentity>
        where Tentity : class
        where Tcontext : DbContext, new()
    {
        public void Add(Tentity entity)
        {
            using(var db = new Tcontext())
            {
                db.Add(entity);
                //db.Set<>().Add(entity);
                //db.Entry(entity).State=EntityState.Added;
                db.SaveChanges();
            }
        }

        public void Delete(Tentity entity)
        {
            using (var db = new Tcontext())
            {
                db.Remove(entity);
                db.SaveChanges();
            }
        }

        public Tentity Get(int id)
        {
            using (var db = new Tcontext())
            {
                var entity = db.Find<Tentity>(id);
                //var entity =db.Set<Tentity>().Find(id);
                return entity ;
            }
        }

        public List<Tentity> GetAll(Expression<Func<Tentity, bool>> filter = null)
        {
            using (var db = new Tcontext())
            {
               //1.yöntem
               // if (filter == null)
               //     return db.Set<Tentity>().ToList();
               // else
               //     return db.Set<Tentity>.Where(filter).ToList();  
               //2.yöntem
                return filter == null?db.Set<Tentity>().ToList() : db.Set<Tentity>().Where(filter).ToList();
            }
        }

        public void Update(Tentity entity)
        {
            using (var db = new Tcontext())
            {
                db.Update(entity);
                db.SaveChanges();
            }
        }
    }
}
