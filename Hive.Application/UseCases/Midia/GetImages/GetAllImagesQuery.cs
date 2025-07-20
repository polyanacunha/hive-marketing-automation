using AutoMapper.Configuration.Conventions;
using Hive.Application.DTOs;
using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Midia.GetImages
{
    public record GetAllImagesQuery() : IRequest<Result<List<ImageResponse>>>
    {
    }
}
