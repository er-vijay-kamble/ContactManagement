namespace ContactManagement.Domain.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class InternalException : Exception
    {
        public InternalException()
        {
        }

        public InternalException(string error) : base($"Error: {error}")
        { }

        public InternalException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InternalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
