using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reduct.Azure.Purview.Account
{
    [Serializable]
    public class AccountDataPlaneCommunicationException : Exception
    {
        public AccountDataPlaneCommunicationException() { }
        public AccountDataPlaneCommunicationException(string message) : base(message) { }
        public AccountDataPlaneCommunicationException(string message, Exception inner) : base(message, inner) { }
        protected AccountDataPlaneCommunicationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
