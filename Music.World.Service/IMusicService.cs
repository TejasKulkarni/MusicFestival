using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Music.World.Service
{
    public interface IMusicService
    {
        Task<string> GetMusicDetails();
    }
}
