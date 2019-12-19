
namespace HighStakes.Client.Models
{

  public class AccountData
  {
    public int AccountId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
  }
  public class PlayerData
  {
    public int UserId { get; set; }
    public int? AccountId { get; set; }
    public AccountData Account { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int ChipTotal { get; set; }
  }
}