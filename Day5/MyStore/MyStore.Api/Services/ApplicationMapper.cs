using AutoMapper;
using MyStore.Api.Contracts;

namespace MyStore.Api.Services
{
    public class ApplicationMapper (IMapper mapper) : IApplicationMapper
    {
        public TDestination Map<TDestination>(object source)
        {
            var re = mapper.Map<TDestination>(source);
            return re;  
        }
    }
}
