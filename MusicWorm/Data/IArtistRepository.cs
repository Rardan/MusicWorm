using MusicWorm.Models;
using System.Collections.Generic;

namespace MusicWorm.Data
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> Artists { get; }

        Artist CreateArtist(Artist artist);
        Artist GetArtistById(int artistId);
        void DeleteArtist(Artist artist);
    }
}