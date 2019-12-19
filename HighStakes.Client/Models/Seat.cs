using System.Collections.Generic;
using HighStakes.Client.Abstracts;

namespace HighStakes.Client.Models
{
  public class DSeat : AGame
  {
    public int SeatId { get; set; }
    public PlayerData Player { get; set; }
    public int ChipTotal { get; set; }
    public List<DCard> Pocket { get; set; }
    public DHand PlayerHand { get; set; }
    public bool BigBlind { get; set; }
    public bool SmallBlind { get; set; }
    public bool Occupied { get; set; }
    public int RoundBid { get; set; }
    public bool Active { get; set; }
    public int HandValue { get; set; }

  }
}