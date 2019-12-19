using System.Collections.Generic;
using HighStakes.Client.Abstracts;

namespace HighStakes.Client.Models
{
  public class TableData : AGame
  {
    public int TableId { get; set; }
    public List<DSeat> Seats { get; set; }
    public DDeck DeckOfCards { get; set; }
    public int SmallBlindAmount { get; set; }
    public int BigBlindAmount { get; set; }
    public List<DSeat> SeatsInTurnOrder { get; set; }
    public bool RoundStarted { get; set; }
  }
}