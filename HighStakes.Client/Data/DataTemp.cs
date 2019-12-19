
using System.Collections.Generic;
using System.Linq;
using HighStakes.Client.Models;

namespace HighStakes.Client.Data
{
  public static class DataTemp
  {
    public static Table table { get; set; }
    private static List<PlayerData> players;

    public static PlayerData GetUserByID(int id)
    {
      if (players == null)
      {
        return null;
      }
      return players.FirstOrDefault(o => o.UserId == id);
    }

    public static void AddUser(PlayerData player)
    {
      if (players == null)
      {
        players = new List<PlayerData>();
      }
      players.Add(player);
    }

    public static Table readData()
    {
      return table;
    }

    public static void writeData(Table table1)
    {
      table = table1;
    }
  }
}