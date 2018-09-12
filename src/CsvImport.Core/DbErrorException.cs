using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Core
{
    public class DbErrorException : InternalServerErrorException
    {
        public DbErrorException()
        {
        }

        public DbErrorException(string message) : base(message)
        {
        }

        public DbErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
