using System;

namespace Oracle.Dapper.NetCore.CustomExceptions
{
    /// <summary>
    /// Classe que controla o tratamento de exceções para registros não encontrados.
    /// </summary>
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException() : base()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}