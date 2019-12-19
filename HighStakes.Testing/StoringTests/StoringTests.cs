using System.Linq;
using HighStakes.Storing.Models;
using HighStakes.Storing.Adapters;
using HighStakes.Storing.Entities;
using HighStakes.Storing.Repositories;
using Xunit;

namespace HighStakes.Testing.StoringTests
{
public class StoringTests{

// Testin  Test Db Context
[Fact]
public void Test_DbContext(){
  HighStakesContextTest db = new HighStakesContextTest();
  db.Database.EnsureCreated();
  var Users = db.User;
  Assert.True(Users.Count()>2);

}
[Fact]
public void Test_DbContextSpecfic(){
  HighStakesContextTest db = new HighStakesContextTest();
  var Users = db.User;
    db.Database.EnsureCreated();
  Assert.True(Users.First(y=>y.FirstName=="Simar").LastName == "Pannu");
}

[Fact]
public void Test_TestDbContextCards(){
  HighStakesContextTest db = new HighStakesContextTest();
  var cards = db.Card;
    db.Database.EnsureCreated();
  Assert.True(cards.Count()==52);

}
///Testing Sql Adapter
[Fact]
public void Test_ContextSwitchingInSqlAdapter(){
  SqlAdapter sa = new SqlAdapter(1);

  Assert.True(sa.getDeck().Cards.Count()==52);
}



[Theory]
[InlineData(1, 100)]
[InlineData(2, 8000)]
public void Test_ChaningChipsSqlAdapter(int id, int chips){
  SqlAdapter sa = new SqlAdapter(1);

  sa.UpdateChips(id, chips);  
  Assert.True(sa.getUsers().FirstOrDefault(x=>x.UserId==id).ChipTotal == chips);
}
//Deck Repository
[Theory]
[InlineData("Hearts", 11)]
[InlineData("Spades", 4)]
public void Test_CardsInDeckRepo(string suit, int value){
  DeckRepository dr = new DeckRepository(1);
  var cards = dr.GetDeck().Cards;
  Assert.True(cards.Exists(x=>x.Suit == suit && x.Value == value));
}
//UserRepo
[Theory]
[InlineData("Simar", "Pannu")]
[InlineData("Han", "Nguyen")]
public void Test_UserExistsinUserRepo(string username, string password){
  UserRepository ur = new UserRepository(1);
  var exists = ur.UserExist(username, password);
  Assert.True(exists);
}
[Fact]
public void Test_ChaningChipsUser(){
 UserRepository ur = new UserRepository(1);
  ur.UpdateChipTotal(1,100);
  Assert.True(ur.GetUsers().FirstOrDefault(x=>x.UserId==1).ChipTotal == 100);
}

[Theory]
[InlineData("Test", "Tester", 1000,"Test", "Tester")]
public void Test_AddingUsersUserRepository(string firstN, string LastN, int Chips, string UserN, string PassN){
 
  UserRepository ur = new UserRepository(1);
  var user = new DUser(){FirstName = firstN, LastName = LastN, ChipTotal = Chips, Account = new DAccount(){UserName = UserN, Password = PassN}};
  ur.addUsertoDatabase(user);
  
  Assert.NotNull(ur.GetUsers().FirstOrDefault(x=>x.FirstName==firstN).AccountId);
}



}    
}