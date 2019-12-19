using System.Collections.Generic;
using HighStakes.Storing.Models;

namespace HighStakes.Storing.Abstracts
{
    public abstract class AGame
    {
        public List<DCard> Flop { get; set; }
    }
}