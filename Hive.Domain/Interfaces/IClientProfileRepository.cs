using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hive.Domain.Entities;

namespace Hive.Domain.Interfaces
{
    public interface IClientProfileRepository
    {
        Task<ClientProfile> Create(ClientProfile clientProfile);
        Task<ClientProfile?> GetById(string id);
    }
}