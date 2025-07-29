using Hive.Application.DTOs;
using Hive.Application.DTOs.Meta;
using Hive.Application.Interfaces;

namespace Hive.Application.Services.Meta.Utils
{
    public class MetaUtilsService : IMetaUtilsService
    {
        private readonly IMetaApiService _metaApiService;

        public MetaUtilsService(IMetaApiService metaApiService)
        {
            _metaApiService = metaApiService;
        }

        public string FormatForMetaApi(DateTime dateTime)
        {
            var offset = new DateTimeOffset(dateTime);
            return offset.ToString("yyyy-MM-ddTHH:mm:sszzz").Replace(":", "");
        }

        
    }
}
