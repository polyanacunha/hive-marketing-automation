using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hive.Application.DTOs;
using MediatR;

namespace Hive.Application.UseCases.Client
{
    public record CreateClientProfileCommand(ClientProfileDTO ClientProfileDTO): IRequest
    {}
}