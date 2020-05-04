using System;
namespace shaker.crosscutting.Exceptions
{
    public class ShakerDomainException : Exception
    {
        public ShakerDomainException(string message)
            : base(message)
        {
        }
    }
}
