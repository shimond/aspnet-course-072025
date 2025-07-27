namespace MyStore.Api.Contracts
{
    public interface IApplicationMapper
    {
        TDestination Map<TDestination>(object source);
    }
}
