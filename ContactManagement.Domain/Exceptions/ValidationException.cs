namespace ContactManagement.Domain.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class ValidationException : Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string error) : base($"Error: {error}")
        { }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
