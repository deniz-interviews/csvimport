using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product
{
    public class ProductFamily : Entity
    {
        public string Key { get; set; }
        public string Category { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
