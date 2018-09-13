using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvImport.Product.Models
{
    public class CsvOperationModel
    {
        public Guid ProductId { get; set; }
        public int Line { get; set; }
        public CsvModel CsvItem { get; set; }
        public Operation Operation { get; set; }

        public CsvOperationModel(CsvModel csvItem, int line)
        {
            CsvItem = csvItem;
            Line = line;
        }

        public CsvOperationModel(Product product, Operation operation)
        {
            ProductId = product.Id;
            CsvItem = new CsvModel
            {
                Brand = product.ProductDetails.FirstOrDefault(x => x.Key == nameof(CsvItem.Brand))?.Value,
                Category = product.ProductFamily.Category,
                Color = product.ProductDetails.FirstOrDefault(x => x.Key == nameof(CsvItem.Color))?.Value,
                Delivery = product.ProductDetails.FirstOrDefault(x => x.Key == nameof(CsvItem.Delivery))?.Value,
                DiscountPrice = product.DiscountPrice,
                FamilyCode = product.ProductFamily.Code,
                Price = product.Price,
                Size = product.ProductDetails.FirstOrDefault(x => x.Key == nameof(CsvItem.Size))?.Value,
                Sku = product.Sku,
                Type = product.ProductDetails.FirstOrDefault(x => x.Key == nameof(CsvItem.Type))?.Value
            };
            Operation = operation;
        }
    }
}
