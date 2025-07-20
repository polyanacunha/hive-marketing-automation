using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.Interfaces
{
    public interface IVideoGenerator
    {
        Task<string> Generator(string prompt, List<string> urlImages);
    }
}
