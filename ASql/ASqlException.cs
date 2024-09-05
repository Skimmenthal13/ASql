using System.Runtime.Serialization;

namespace ASql
{
    [Serializable]
    internal class ASqlException : Exception
    {
        public ASqlException()
        {
        }

        public ASqlException(string message) : base(message)
        {
        }

        public ASqlException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ASqlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}