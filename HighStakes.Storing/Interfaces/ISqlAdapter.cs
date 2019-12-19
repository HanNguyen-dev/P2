using System.Collections.Generic;
using HighStakes.Storing.Models;


namespace HighStakes.Storing.Interfaces
{

  public interface ISqlAdapter
  {
   

  DDeck getDeck();
  List<DUser> getUsers(); 

  DDeck BuildDeck();
  List<DUser> BuildUsers();
  void addUser(DUser user);
  void UpdateChips(int UserId, int Chips);

}
}
