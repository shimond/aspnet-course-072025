namespace MyStore.Api.Exceptions;



[Serializable]
public class ItemNotFoundException : Exception
{
    public int ItemId { get; set;}
    public ItemNotFoundException() { }
	public ItemNotFoundException(string message, int itemId) : base(message) {
        ItemId = itemId;
    }
	public ItemNotFoundException(string message, Exception inner) : base(message, inner) { }
	protected ItemNotFoundException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
