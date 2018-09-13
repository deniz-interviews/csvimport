using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CsvImport.Product.Models
{
    public class CsvModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Sku { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FamilyCode { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Brand { get; set; }

        [Required]
        public double Price { get; set; }

        public double? DiscountPrice { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Delivery { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Category { get; set; }
        
        public string Size { get; set; }
        
        public string Color { get; set; }
    }
}
