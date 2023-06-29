using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaNegocio.DominioException
{
    public class DominioException : Exception
    {
        /// <summary>
        /// Excepcion que se arroja tras una falla al intentar acceen el dominio
        /// </summary>
        public DominioException() { }
        public DominioException(string message) : base(message) { }
        public DominioException(string message, Exception ex) : base(message, ex) { }

    }
}
