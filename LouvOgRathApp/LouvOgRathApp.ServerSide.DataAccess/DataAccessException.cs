using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouvOgRathApp.ServerSide.DataAccess
{
    [Serializable]
    public class DataAccessException : Exception
    {
        /// <summary>
        /// used for DataAccess exceptions. Remeber to use it.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public DataAccessException(string message, Exception inner) : base(message, inner) { }
    }
}
