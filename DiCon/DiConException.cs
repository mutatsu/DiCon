using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiCon
{
    public class DiConXMLException : Exception
    {
        public DiConXMLException() { }
        public DiConXMLException(string message) : base(message) { }
        public DiConXMLException(string message, Exception inner) : base(message) { }
    }
    public class DiConNetworkException : Exception
    {
        public DiConNetworkException() { }
        public DiConNetworkException(string message) : base(message) { }
        public DiConNetworkException(string message, Exception inner) : base(message) { }
    }
    public class DiConMSWrodException : Exception
    {
        public DiConMSWrodException() { }
        public DiConMSWrodException(string message) : base(message) { }
        public DiConMSWrodException(string message, Exception inner) : base(message) { }
    }
}
