using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCRM.Layered.Model.Utility
{
    public class CustomeException : Exception
    {
        public CustomeException()
        {
        }

        public CustomeException(string message)
            : base(message)
        {
        }

        public CustomeException(string criticalMessage, string message , bool logException , Exception inner , ref string errorMessage )
            : base(message, inner)
        {
            errorMessage = message;
            // log Error Here :  
        } 
        public CustomeException(string message, bool logException , Exception inner  )
            : base(message, inner)
        {

            // log Error Here :  
        }
    }
}