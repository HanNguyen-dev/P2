using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using HighStakes.Client.Data;

namespace HighStakes.Client.Models
{
  public class JoinTable
  {
    [Required]
    public int userID { get; set; }
    [Required]
    public int buyIn { get; set; }
    public PlayerData user { get; set; }


    // Duplicate codes, refactor later
    public void LoadUser()
    {
      this.user = DataTemp.GetUserByID(this.userID);
    }
  }
}

