using PriceCheck.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.Data.Entities
{
    public partial class Shop : BaseEntity, IShop
    {
        public string? ProductName { get; set; }
        public string? ProductPrice { get; set; }
        public string ProductLink { get; set; }
    }
}
