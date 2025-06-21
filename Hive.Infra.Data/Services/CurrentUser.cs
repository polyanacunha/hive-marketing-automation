using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hive.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Hive.Infra.Data.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var idClaim = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(idClaim))
                {
                    return null;
                }

                if (!Guid.TryParse(idClaim, out var userId))
                {
                    return null;
                }
            
                return userId;
            }
        }
    }
}