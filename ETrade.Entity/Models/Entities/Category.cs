using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Entity.Models.Entities
{
    public class Category : Entity
    {
        public Category() : base () 
        {
        }

        public Category(string name) : base(name)
        {
        }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
