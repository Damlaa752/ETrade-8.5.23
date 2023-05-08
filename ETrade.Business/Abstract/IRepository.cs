using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Business.Abstract
{
    public interface IRepository<Tentity> where Tentity : class 
    {
        void Add(Tentity tentity);
        void Update(Tentity tentity);
        void Delete(Tentity tentity);
        List<Tentity> GetAll(Expression<Func<Tentity,bool>>filter=null);//home>index isHome olan bütün ürünler 
        Tentity Get(int id);
    }
}
