using System;

namespace TransactionApplication.Infrastructure.Exceptions
{
    public class ValidatonException : Exception
    {
        public ValidatonException()
            : base()
        {
        }

        public ValidatonException(string message)
            : base(message)
        {
        }

        public ValidatonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
