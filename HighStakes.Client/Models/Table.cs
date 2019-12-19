using System;
using System.Linq;
using System.Collections.Generic;
using HighStakes.Client.HTTPClient;

namespace HighStakes.Client.Models
{
  [Serializable]
  public class Table
  {
    public TableData table { get; set; }
    public int nextTurn { get; set; }
    public int HighBid { get; set; }
    public int subround { get; set; }
    public List<DSeat> seatsOrder { get; set; }
    public int PotValue { get; set; }

    public void LoadTable()
    {
      HighStakesHttpClient httpClient = new HighStakesHttpClient();
      this.table = httpClient.RunAsyncForTable().GetAwaiter().GetResult();
      Console.WriteLine("HanError: in load table");
    }

    public int NumOfActivePlayers()
    {
      int num = 0;
      foreach (DSeat seat in table.Seats)
      {
        if (seat.Occupied && seat.Active)
        {
          num++;
        }
      }
      return num;
    }

    public void JoinGame(PlayerData inputPlayer)
    {
      HighStakesHttpClient httpClient = new HighStakesHttpClient();
      this.table = httpClient.RunAsyncForJoinTable(this.table, inputPlayer).GetAwaiter().GetResult();
    }

    public void StartRound()
    {
      HighStakesHttpClient httpClient = new HighStakesHttpClient();
      this.table = httpClient.RunAsyncForStartRound(this.table).GetAwaiter().GetResult();

      this.subround = 0;
      this.HighBid = this.table.SmallBlindAmount;

      Reorder();

      // this.seatsOrder = this.table.SeatsInTurnOrder;
      this.PotValue = this.table.SmallBlindAmount + this.table.BigBlindAmount;
    }

    public void EndRound()
    {
      HighStakesHttpClient httpClient = new HighStakesHttpClient();
      this.table = httpClient.RunAsyncForEndRound(this.table).GetAwaiter().GetResult();
    }

    public void StartGame()
    {
      HighStakesHttpClient httpClient = new HighStakesHttpClient();
      this.table = httpClient.RunAsyncForStartGame(this.table).GetAwaiter().GetResult();
    }

    public void Reorder()
    {
      var i = 0;
      this.seatsOrder = new List<DSeat>();
      foreach (DSeat seat in this.table.SeatsInTurnOrder)
      {
        if (seat.Occupied)
        {
          seat.SeatId = 0;
          this.seatsOrder.Add(seat);
          i++;
        }
      }
    }
    public bool incrementTurn()
    {
      if (nextTurn < this.NumOfActivePlayers() - 1)
      {
        Console.WriteLine("From Turn true");
        nextTurn++;
        return true;
      } else
      {
        Reorder();
        this.subround++;
        if (this.subround == 4) {

          this.subround = 0;
          foreach (var seat in seatsOrder)
          {
            seat.Active = true;
            seat.Flop.AddRange(this.table.Flop);

            DSeat currentSeat = table.Seats.FirstOrDefault(o => o.Player.UserId == seat.Player.UserId);
            int index = table.Seats.IndexOf(currentSeat);
            table.Seats[index] = seat;

            currentSeat = table.SeatsInTurnOrder.FirstOrDefault(o => o.Player.UserId == seat.Player.UserId);
            index = table.Seats.IndexOf(currentSeat);
            table.SeatsInTurnOrder[index] = seat;

          }

          EndRound();
          StartRound();
        }
        Console.WriteLine("From Turn false");
        nextTurn = 0;
        return false;
      }
    }
    public void Bid(int userId, int bid)
    {
      // HighStakesHttpClient httpClient = new HighStakesHttpClient();
      // seat = httpClient.RunAsyncForBid(seat, bid).GetAwaiter().GetResult();
      DSeat seat = seatsOrder.FirstOrDefault(o => o.Player.UserId == userId);
      if (bid > seat.ChipTotal)
      {
        seat.RoundBid += seat.ChipTotal;
        seat.ChipTotal = 0;
      } else {
        seat.RoundBid += bid;
        seat.ChipTotal -= bid;
      }
    }
  }
}

