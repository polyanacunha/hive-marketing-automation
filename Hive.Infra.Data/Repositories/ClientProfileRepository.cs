using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hive.Infra.Data.Repositories
{
    public class ClientProfileRepository : IClientProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClientProfile> Create(ClientProfile clientProfile)
        {
            await _context.ClientProfile.AddAsync(clientProfile);
            return clientProfile;
        }

        public async Task<ClientProfile?> GetById(Guid id)
        {
            return await _context.ClientProfile
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}