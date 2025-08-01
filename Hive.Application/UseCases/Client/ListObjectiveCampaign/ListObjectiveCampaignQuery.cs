using Hive.Domain.Entities;
using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Client.ListObjectiveCampaign
{
    public record ListObjectiveCampaignQuery : IRequest<Result<List<ObjectiveCampaign>>>
    {
    }
}
