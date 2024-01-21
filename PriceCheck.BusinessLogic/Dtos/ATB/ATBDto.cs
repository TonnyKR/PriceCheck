using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.BusinessLogic.Dtos.ATB
{
    public class ATBDto : IShopDto
    {
        public int? Id { get; set; }
        [MaxLength(200)]
        [DataType(DataType.Text)]
        public string? ProductName { get; set; }
        [MaxLength(10)]
        [DataType(DataType.Text)]
        public string? ProductPrice { get; set; }
        [Required]
        [MaxLength(200)]
        [DataType(DataType.Text)]
        public string ProductLink { get; set; }
    }
}
