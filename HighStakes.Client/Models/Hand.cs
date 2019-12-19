using System.Collections.Generic;

namespace HighStakes.Client.Models
{
  public class DHand
  {
    public int HandId { get; set; }
    public List<DCard> HandCards { get; set; }
    public int HandValue { get; set; }

  }
}