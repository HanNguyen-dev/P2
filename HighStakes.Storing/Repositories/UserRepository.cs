using HighStakes.Storing.Models;
using System.Collections.Generic;
using HighStakes.Storing.Adapters;
using HighStakes.Storing.Interfaces;

namespace HighStakes.Storing.Repositories
{
  public class UserRepository
  {
    readonly List<DUser> _Users;
    readonly ISqlAdapter sa;
    public DUser GetUser(string username, string password) { return GetUsers().Find(x=>x.Account.UserName== username && x.Account.Password==password);}
    public DUser GetUser(int id) { return _Users.Find(x=>x.UserId == id);}
    public List<DUser> GetUsers(){return sa.getUsers();}
    public UserRepository()
    {
      sa = new SqlAdapter();
      _Users = sa.getUsers();
    }
        public UserRepository(int i)
    {
      sa = new SqlAdapter(i);
      _Users = sa.getUsers();
    }
    public void UpdateChipTotal(int UserId, int Chips){
      _Users.Find(x=>x.UserId==UserId).ChipTotal = Chips;
      sa.UpdateChips(UserId, Chips);
     
    }
    public void addUsertoDatabase(DUser User){
      sa.addUser(User);
     _Users.Add(User);
    }
    public bool UserExist(string username, string password){
    
      foreach (var item in _Users)
      {
          if(item.Account.UserName == username && item.Account.Password == password){return true;}
      }
      return false;
    }

    
  }
}
