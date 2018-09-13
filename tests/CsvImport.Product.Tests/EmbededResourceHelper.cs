using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CsvImport.Product
{
    public static class EmbededResourceHelper
    {
        public static Stream Get(string name)
        {
            // TODO: add more test csv's
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream("CsvImport.Product.TestCsvFiles." + name);
        }
    }
}
