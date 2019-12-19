using HighStakes.Client.Controllers;
using HighStakes.Client.Models;
using HighStakes.Storing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;

namespace HighStakes.Testing.ClientTests
{
  public class HomeControllerTest
  {
    private readonly ILogger<HomeController> logger = LoggerFactory.Create(o => o.SetMinimumLevel(LogLevel.Debug)).CreateLogger<HomeController>();

    [Fact]
    public void Test_IndexPage()
    {
      var home = new HomeController(logger);
      var view = home.Index();

      Assert.NotNull(view);
    }


    // [Theory]
    // [InlineData("Han","Nguyen")]
    // // [InlineData(new Player(){username = "Jim", password = "Boo"})]

    // public void Test_LobbyPage(string user, string pass)
    // {
    //   Player player = new Player(){username = user, password = pass};
    //   var home = new HomeController(logger);
    //   var view = home.Lobby(player);

    //   Assert.NotNull(view);
    // }

    // [Theory]
    // [InlineData(1, 1000)]
    // public void Test_TablePage(int userId, int buyIn)
    // {
    //   JoinTable player = new JoinTable(){userID = userId, buyIn = buyIn};

    //   var home = new HomeController(logger);
    //   var view = home.Table(player);

    //   Assert.NotNull(view);
    // }


    [Fact]
    public void Test_PaymentPage()
    {
      var home = new HomeController(logger);
      var view = home.Payment();

      Assert.NotNull(view);
    }

    // [Fact]
    // public void Test_UpdatePage()
    // {
    //   var home = new HomeController(logger);
    //   var jsonString = home.Update();

    //   Assert.True(jsonString.GetType() == typeof(string));
    // }

    // [Theory]
    // [InlineData("1", "10")]

    // public void Test_BidMethod(string userId, string bid)
    // {
    //   var home = new HomeController(logger);
    //   var emptyString = home.Bid(userId, bid);

    //   Assert.True(emptyString.GetType() == typeof(string));
    //   Assert.True(string.IsNullOrEmpty(emptyString));
    // }

    [Fact]
    public void Test_PrivacyPage()
    {
      var home = new HomeController(logger);
      var view = home.Privacy();

      Assert.NotNull(view);
    }

    // [Fact]
    // public void Test_ErrorPage()
    // {
    //   var home = new HomeController(logger);
    //   var view = home.Error();

    //   Assert.NotNull(view);
    // }

  }
}
