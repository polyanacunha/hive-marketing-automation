using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Campaigns.ListPages
{
    public record ListPagesQuery : IRequest<Result<string>>
    {
    }
}
