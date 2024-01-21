using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.BusinessLogic.Dtos.ATB
{
    public class ATBUpdateDto : IShopUpdateDto
    {
        [MaxLength(200)]
        public string? ProductName { get; set; }
        [MaxLength(10)]
        public string? ProductPrice { get; set; }
        [MaxLength(200)]
        public string? ProductLink { get; set; }
    }
}
