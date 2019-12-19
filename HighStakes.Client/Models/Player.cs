using System.ComponentModel.DataAnnotations;
using HighStakes.Client.HTTPClient;

namespace HighStakes.Client.Models
{
  public class Player
  {
    public PlayerData user { get; set; }
    public int userID { get; set; }

    [StringLength(50)]
    [Required]
    public string username { get; set; }
    [StringLength(50)]
    [Required]
    public string password { get; set; }
    public string firstname { get; set; }
    public string lastname { get; set; }
    public int chip { get; set; }

    public void LoadUser()
    {
      HighStakesHttpClient httpClient = new HighStakesHttpClient();
      this.user = httpClient.RunAsyncForUser(this.username, this.password).GetAwaiter().GetResult();
      this.userID = this.user.UserId;
    }
  }
}