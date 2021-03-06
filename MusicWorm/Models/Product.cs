﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string AlbumDescription { get; set; }
        public int ArtistId { get; set; }
        public DateTime AlbumDating { get; set; }
        public string Photo { get; set; }

        public Artist Artist { get; set; }
        public Storage Storage { get; set; }
    }
}
