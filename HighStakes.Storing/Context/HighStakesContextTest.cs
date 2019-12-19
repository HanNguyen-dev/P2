using System.Collections.Generic;
using HighStakes.Storing.Models;
using Microsoft.EntityFrameworkCore;
using HighStakes.Storing.Interfaces;



namespace HighStakes.Storing.Entities
{
  public class HighStakesContextTest : DbContext, IHighStakesContext
  {


    protected override void OnConfiguring(DbContextOptionsBuilder dbContext)
    {
      dbContext.UseInMemoryDatabase("TestDatabase");
    }


    public DbSet<DAccount> Account { get; set; }
    public DbSet<DCard> Card { get; set; }
    public DbSet<DUser> User { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<DCard>(o => o.HasKey(k => k.CardId));
      modelBuilder.Entity<DCard>().Property(p => p.CardId).UseSerialColumn();
      modelBuilder.Entity<DCard>().HasData(BuildDeck());

      modelBuilder.Entity<DUser>(o => o.HasKey(k => k.UserId));
      modelBuilder.Entity<DUser>().Property(p => p.UserId).UseSerialColumn();
      modelBuilder.Entity<DUser>().HasOne(p => p.Account);
      modelBuilder.Entity<DUser>().HasData(BuildUser());

      modelBuilder.Entity<DAccount>(o => o.HasKey(k => k.AccountId));
      modelBuilder.Entity<DAccount>().Property(p => p.AccountId).UseSerialColumn();
      modelBuilder.Entity<DAccount>().HasData(BuildAccount());
    }
    public List<DCard> BuildDeck()
    {
      List<DCard> cards = new List<DCard>();
      var cardId = 1;
      foreach (var suit in new[] { "Spades", "Hearts", "Clubs", "Diamonds", })
      {
        for (var Value = 2; Value <= 14; Value++)
        {
          cards.Add(new DCard(cardId, Value, suit));
          cardId++;
        }
      }
     
      return cards;
    }
    public List<DAccount> BuildAccount()
    {
      return new List<DAccount>(){
      new DAccount(){AccountId=1,UserName="Simar",Password="Pannu"},
      new DAccount(){AccountId=2,UserName="Han",Password="Nguyen"},
      new DAccount(){AccountId=3,UserName="James",Password="Goldsmith"}};

    }
    public List<DUser> BuildUser()
    {
      return new List<DUser>(){
      new DUser(){UserId =1, FirstName="Simar", LastName="Pannu", ChipTotal=5000, AccountId=1},
      new DUser(){UserId =2, FirstName="Han", LastName="Nguyen", ChipTotal=5000, AccountId=2},
      new DUser(){UserId =3, FirstName="James", LastName="Goldsmith", ChipTotal=5000, AccountId=3}};

    }
  }
}


