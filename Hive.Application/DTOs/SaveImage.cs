using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.DTOs
{
    public record SaveImage(string ClientId,Stream File, string FileName, string AlbumName)
    {
    }
}
