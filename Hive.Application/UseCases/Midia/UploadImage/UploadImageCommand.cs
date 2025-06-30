using Hive.Application.DTOs;
using Hive.Domain.Validation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Midia.UploadImage
{
    public record UploadImageCommand(string AlbumName, List<IFormFile> Files) : IRequest<Result<List<UploadedImageDto>>>
    {

    }
}
