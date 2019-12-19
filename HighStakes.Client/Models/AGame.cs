using System.Collections.Generic;
using HighStakes.Client.Models;

namespace HighStakes.Client.Abstracts
{
  public abstract class AGame
  {
    public List<DCard> Flop { get; set; }
  }
}