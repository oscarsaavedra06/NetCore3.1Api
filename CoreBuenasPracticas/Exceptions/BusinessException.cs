using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBuenasPracticas.Exceptions
{
    public class BusinessException : Exception   //clase de excepciones, para que el API no responda un error de trace.
    {
        public BusinessException()
        {

        }

        public BusinessException(string message): base(message)
        {
                
        }
    }
}
