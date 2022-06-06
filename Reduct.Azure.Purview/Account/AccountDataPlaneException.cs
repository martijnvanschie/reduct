namespace Reduct.Azure.Purview.Account
{
    [Serializable]
    public class AccountDataPlaneException : Exception
    {
        public AccountDataPlaneException() { }
        public AccountDataPlaneException(string message) : base(message) { }
        public AccountDataPlaneException(string message, Exception inner) : base(message, inner) { }
        protected AccountDataPlaneException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
