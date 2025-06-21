using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Midia.CreateVideo
{
    public record CreateVideoCommand(
        string AlbumName,
        List<Stream> Files
        ) : IRequest<Result<int>>
    {
    }
}
