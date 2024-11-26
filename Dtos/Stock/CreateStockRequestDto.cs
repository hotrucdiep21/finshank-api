using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Stock's symbol must be 3 characters")]
        [MaxLength(5, ErrorMessage = "Stock's symbol can not be over 5 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(50, ErrorMessage = "Company name can not be over 50 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDev { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Industry can not be over 50 characters")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }

    }
}