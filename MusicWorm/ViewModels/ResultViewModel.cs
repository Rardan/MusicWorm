using MusicWorm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.ViewModels
{
    public class ResultViewModel
    {
        public string Name { get; set; }
        public PartialResult Result1 { get; set; }
        public PartialResult Result10 { get; set; }
        public PartialResult Result20 { get; set; }
        public PartialResult Result50 { get; set; }
        public PartialResult Result100 { get; set; }
    }
}
