using System.Collections.Generic;
using System.Linq;
using HighStakes.Storing.Entities;
using HighStakes.Storing.Interfaces;
using HighStakes.Storing.Models;

namespace HighStakes.Storing.Adapters
{

  public class SqlAdapter: ISqlAdapter
  {
   
    readonly IHighStakesContext _context;

    DDeck _Deck {get; set;}
    List<DUser> _Users {get; set;}
    public DDeck getDeck() { return _Deck;}
    public List<DUser> getUsers() {return BuildUsers();}

    public SqlAdapter(){
      HighStakesContext db =new HighStakesContext();
    _context = db;
    _Deck = BuildDeck();
    _Users = BuildUsers();
    }
    public SqlAdapter(int i){
     
   HighStakesContextTest db = new HighStakesContextTest(); // _context.
   db.Database.EnsureCreated();
   _context = db;
   _Deck = BuildDeck();
   _Users = BuildUsers();
    }
    

    public DDeck BuildDeck(){

      List<DCard> cards = new List<DCard>();
      foreach (var card in _context.Card)
      {
          cards.Add(card);
      }
   //   _context.Dispose
      return new DDeck(0, cards);
    }
    public List<DUser> BuildUsers(){
     
    List<DUser> _tempUsers= new List<DUser>();
    List<DAccount> _Accounts = new List<DAccount>();
    foreach (var a in _context.Account)
    {
        _Accounts.Add(new DAccount(){AccountId=a.AccountId,UserName=a.UserName,Password=a.Password});
    }
        foreach (var u in _context.User)
    {
       _tempUsers.Add(new DUser(){UserId= u.UserId,FirstName =u.FirstName, LastName =u.LastName, ChipTotal= u.ChipTotal, AccountId = u.AccountId});
    }
    foreach (var user in _tempUsers)
    {
        foreach (var account in _Accounts)
        {
            if(user.AccountId == account.AccountId){user.Account = account;}
        }
    }
    
    return _tempUsers;
  }
  public void addUser(DUser user){
    _Users.Add(user);
    var u = new DUser(){FirstName = user.FirstName, LastName = user.LastName, ChipTotal = user.ChipTotal};
    var a = new DAccount(){UserName = user.Account.UserName, Password = user.Account.Password};
    _context.Account.Add(a);
    u.AccountId = a.AccountId;
    _context.User.Add(u);
    _context.SaveChanges();
  }
  public void UpdateChips(int UserId, int Chips){
    _Users.First(x=>x.UserId==UserId).ChipTotal = Chips;
   _context.User.First(x=>x.UserId==UserId).ChipTotal = Chips;
    _context.SaveChanges();
  }


  }
}
