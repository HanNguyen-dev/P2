using HighStakes.Storing.Models;
using Microsoft.EntityFrameworkCore;

namespace HighStakes.Storing.Interfaces
{
  public interface IHighStakesContext
  {
    DbSet<DAccount> Account { get; set; }
    DbSet<DCard> Card { get; set; }
    DbSet<DUser> User { get; set; }
    int SaveChanges();
  }
}