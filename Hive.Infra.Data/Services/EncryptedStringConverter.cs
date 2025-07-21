using Hive.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Services
{
    public class EncryptedStringConverter : ValueConverter<string, string>
    {
        public EncryptedStringConverter(IEncryptionService encryptionService)
        : base(
            v => encryptionService.Encrypt(v),
            v => encryptionService.Decrypt(v))
        { }
    }
}
