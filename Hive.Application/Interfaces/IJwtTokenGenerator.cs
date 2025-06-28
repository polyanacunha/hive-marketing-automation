using Hive.Application.DTOs;
using Hive.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public (string JwtToken, DateTime expiresAtUtc) GenerateJwtToken(InfoUser user);
        public string GenerateRefreshToken();
        public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token, DateTime expiration);

    }
}
