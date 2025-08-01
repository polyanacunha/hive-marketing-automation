using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Campaigns.Meta.ListPages
{
    public record ListPagesQuery(string AccountId) : IRequest<Result<string>>
    {
    }
}
