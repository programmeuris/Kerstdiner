using System;
using System.Collections.Generic;
using System.Text;

namespace Kerstdiner.Models
{
    public class CustomException : Exception
    {
        public CustomException() { }

        public CustomException(string message) : base(message) { }
    }

    public class ValueZeroOrNegativeException : CustomException
    {
        public ValueZeroOrNegativeException() { }

        // supply the name of the thing that caused the exception and it will update the message to use that name
        public ValueZeroOrNegativeException(string trigger) : base($"{trigger} moet een positief getal zijn!") { }
    }

    public class ValueRequiredException : CustomException
    {
        public ValueRequiredException() { }

        // supply the name of the thing that caused the exception and it will update the message to use that name
        public ValueRequiredException(string trigger) : base($"{trigger} is niet ingevuld!") { }
    }
}
