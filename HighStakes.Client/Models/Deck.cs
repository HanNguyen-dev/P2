using System.Collections.Generic;

namespace HighStakes.Client.Models
{
  public class DDeck
  {
    public int DeckId { get; set; }
    public List<DCard> Cards { get; set; }

  }
}