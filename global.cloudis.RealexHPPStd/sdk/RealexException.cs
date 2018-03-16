using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace global.cloudis.RealexHPP.sdk
{
    public class RealexException : Exception
    {
        public RealexException(string message) : base(message)
        {

        }
        public RealexException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
