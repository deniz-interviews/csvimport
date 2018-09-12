using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Product
{
    public interface IProductManager
    {
        Task<Attempt<Product>> ImportCsvAsync(Stream file, CancellationToken cancellationToken = default(CancellationToken));
    }
}
