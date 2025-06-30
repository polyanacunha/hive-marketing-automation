using Hive.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Services
{
    public class PikaLabsGenerator : IVideoGenerator
    {
        public Task<string> Generator(string prompt, List<string> urlImages)
        {
            return Task.FromResult("https://teste.com");
        }
    }
}
