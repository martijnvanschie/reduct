namespace Reduct.Azure.Purview.Account
{
    [Serializable]
    public class CollectionNotFoundException : Exception
    {
        public CollectionNotFoundException() { }
        public CollectionNotFoundException(string message) : base(message) { }
        public CollectionNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected CollectionNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
