using ETrade.Business.Concrete;
using ETrade.DAL.Abstract;
using ETrade.DAL.Context;
using ETrade.Entity.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DAL.Concrete
{
    public class OrderlineDAL : Repository<ETradeDbContext, OrderLine> , IOrderlineDAL
    {
    }
}
