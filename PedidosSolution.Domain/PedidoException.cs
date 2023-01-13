using System;
using System.Runtime.Serialization;

namespace PedidosSolution.Domain
{
    [Serializable]
    internal class PedidoException : Exception
    {
        public PedidoException()
        {
        }

        public PedidoException(string message) : base(message)
        {
        }

        public PedidoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PedidoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}