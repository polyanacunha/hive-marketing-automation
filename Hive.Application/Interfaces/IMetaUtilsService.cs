using Hive.Application.DTOs.Meta;
using Hive.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.Interfaces
{
    public interface IMetaUtilsService
    {
        
        Task<LocationsMeta> MapLocationsMeta(List<Location> locations, string accessToken);
        public string FormatForMetaApi(DateTime dateTime);
    }
}
