using Microsoft.EntityFrameworkCore;
using MusicWorm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Data
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly WormDbContext _wormDbContext;

        public ArtistRepository(WormDbContext wormDbContext)
        {
            _wormDbContext = wormDbContext;
        }

        public IEnumerable<Artist> Artists => _wormDbContext.Artists;
        public Artist GetArtistById(int artistId) => _wormDbContext.Artists.FirstOrDefault(a => a.Id == artistId);
        
        public Artist CreateArtist(Artist artist)
        {
            _wormDbContext.Add(artist);
            _wormDbContext.SaveChanges();
            return artist;
        }
        
        public void DeleteArtist(Artist artist)
        {
            _wormDbContext.Remove(artist);
            _wormDbContext.SaveChanges();
        }
    }
}
