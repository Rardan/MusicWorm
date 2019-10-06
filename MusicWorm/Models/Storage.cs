using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        
        public Product Product { get; set; }
    }
}
