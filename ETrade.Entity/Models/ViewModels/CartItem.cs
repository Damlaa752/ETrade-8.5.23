﻿using ETrade.Entity.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Entity.Models.ViewModels
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quatity { get; set; }
    }
}
