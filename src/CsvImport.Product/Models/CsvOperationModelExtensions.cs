using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CsvImport.Product.Models
{
    public static class CsvOperationModelExtensions
    {
        public static Product ToProduct(this CsvOperationModel csvOperationModel, Product product = null)
        {
            return csvOperationModel.ToProduct(product.ProductFamily, product);
        }

        public static Product ToProduct(this CsvOperationModel csvOperationModel, ProductFamily productFamily = null, Product product = null)
        {
            if (product == null)
            {
                product = new Product();
                product.ProductDetails = new List<ProductDetail>();
            }

            product.Price = csvOperationModel.CsvItem.Price;
            product.DiscountPrice = csvOperationModel.CsvItem.DiscountPrice;
            product.Sku = csvOperationModel.CsvItem.Sku;

            if (productFamily != null)
                product.ProductFamily = productFamily;
            else
            {
                product.ProductFamily = new ProductFamily
                {
                    Category = csvOperationModel.CsvItem.Category,
                    Code = csvOperationModel.CsvItem.FamilyCode
                };
            }

            UpdateDetails(product, nameof(csvOperationModel.CsvItem.Brand), csvOperationModel.CsvItem.Brand);
            UpdateDetails(product, nameof(csvOperationModel.CsvItem.Color), csvOperationModel.CsvItem.Color);
            UpdateDetails(product, nameof(csvOperationModel.CsvItem.Delivery), csvOperationModel.CsvItem.Delivery);
            UpdateDetails(product, nameof(csvOperationModel.CsvItem.Size), csvOperationModel.CsvItem.Size);
            UpdateDetails(product, nameof(csvOperationModel.CsvItem.Type), csvOperationModel.CsvItem.Type);

            return product;
        }

        private static void UpdateDetails(Product product, string key, string value)
        {
            var existingDetail = product.ProductDetails.FirstOrDefault(pd => pd.Key == key);
            if (!string.IsNullOrEmpty(value))
            {
                if (existingDetail != null)
                    existingDetail.Value = value;
                else
                    product.ProductDetails.Add(new ProductDetail { Key = key, Value = value });
            }
            else if (existingDetail != null)
            {
                product.ProductDetails.Remove(existingDetail);
            }
        }

        public static bool TryValidate(this IEnumerable<CsvOperationModel> csvOperations, out IDictionary<CsvOperationModel, ICollection<ValidationResult>> validationResults)
        {
            validationResults = new Dictionary<CsvOperationModel, ICollection<ValidationResult>>();

            foreach (var csvOperation in csvOperations)
            {
                validationResults.Add(csvOperation, new List<ValidationResult>());

                ICollection<ValidationResult> itemValidationResult;
                if (!csvOperation.TryValidate(out itemValidationResult))
                {
                    validationResults[csvOperation] = itemValidationResult;
                    continue;
                }

                if (csvOperations.Count(i => i.CsvItem.Sku == csvOperation.CsvItem.Sku) > 1)
                    validationResults[csvOperation].Add(new ValidationResult($"SKU {csvOperation.CsvItem.Sku} is not unique", new string[] { nameof(csvOperation.CsvItem.Sku) }));

                var familyCode = csvOperation.CsvItem.FamilyCode;
                var category = csvOperation.CsvItem.Category;

                if (familyCode == "7")
                    familyCode = csvOperation.CsvItem.FamilyCode;


                var sameFamilyCodeItems = csvOperations.Where(i => i.CsvItem.FamilyCode == familyCode);

                if (sameFamilyCodeItems.Any(sf => sf.CsvItem.Category != category))
                    validationResults[csvOperation].Add(new ValidationResult($"Inconsistent FamilyCode {familyCode} and Category {category}",
                        new string[] { nameof(csvOperation.CsvItem.FamilyCode), nameof(csvOperation.CsvItem.Category) }));
            }

            return !validationResults.Values.Any();
        }

        private static bool TryValidate(this CsvOperationModel csvOperation, out ICollection<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(csvOperation, new ValidationContext(csvOperation), validationResults))
                return false;

            if (csvOperation.CsvItem.Price == 0)
                validationResults.Add(new ValidationResult("Price cannot be empty or zero", new string[] { nameof(csvOperation.CsvItem.Price) }));

            if (csvOperation.CsvItem.DiscountPrice == 0)
                validationResults.Add(new ValidationResult("Discounted Price cannot be empty or zero", new string[] { nameof(csvOperation.CsvItem.DiscountPrice) }));

            if (csvOperation.CsvItem.DiscountPrice > csvOperation.CsvItem.Price)
                validationResults.Add(new ValidationResult("Discounted Price cannot be larger than Price", new string[] { nameof(csvOperation.CsvItem.DiscountPrice) }));

            return !validationResults.Any();
        }


    }
}
