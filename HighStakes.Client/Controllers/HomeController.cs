using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HighStakes.Client.Models;
using Newtonsoft.Json;
using HighStakes.Client.Data;
// using HighStakes.Client.Data;
// using HighStakes.Client.Models;

namespace HighStakes.Client.Controllers
{

  [Route("/[controller]/[action]")]
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpPost()]
    public IActionResult Lobby(Player player)
    {
      if (!ModelState.IsValid)
      {
        return View("Index");
      }

      player.LoadUser();

      if (player.user == null)
      {
        return RedirectToAction("Index", "Home");
      }
      // DataTemp dt = new DataTemp();
      DataTemp.AddUser(player.user);
      ViewData["userTemp"] = player.userID;
      return View();

    }

    public IActionResult Payment()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Table(JoinTable newPlayer)
    {
      Console.WriteLine($"HanError: " + newPlayer.userID);

      Table tableOne = DataTemp.readData();

      if (tableOne == null)
      {
        tableOne = new Table();
        tableOne.LoadTable();
        tableOne.nextTurn = 0;
        Console.WriteLine("HanError: from Action Table 1");
      }

      if (tableOne.NumOfActivePlayers() >= 6)
      {
        // room is full
        return View("Index");
      }
      newPlayer.LoadUser();

      Console.Write("HanError: First");
      Console.WriteLine(newPlayer.user.FirstName);

      if (tableOne.table.Seats.FirstOrDefault(o => o.Player.UserId == newPlayer.userID) == null)
      {

        tableOne.JoinGame(newPlayer.user);

        if (tableOne.NumOfActivePlayers() == 2) {

          tableOne.StartGame();
          tableOne.StartRound();
        }
      }

      DataTemp.writeData(tableOne);

      return View("Table", tableOne);
    }

    public IActionResult Register()
    {
      return View();
    }



    [HttpGet]
    public string Update()
    {
      return JsonConvert.SerializeObject(DataTemp.readData());
    }

    [HttpGet("{userId}/{bid}")]
    public string Bid(string userId, string bid)
    {

      int intUserId;
      int intBid;
      if (!Int32.TryParse(userId, out intUserId) || !Int32.TryParse(bid, out intBid))
      {
        return null;
      }

      Table table = DataTemp.readData();
      // DSeat currentSeat = table.seatsOrder.FirstOrDefault(o => o.Player.UserId == intUserId);
      table.Bid(intUserId, intBid);

      table.PotValue += intBid;

      table.incrementTurn();

      DataTemp.writeData(table);
      return "";
    }
    [HttpGet("{userId}/{bid}")]
    public string Raise(string userId, string bid)
    {
      int intUserId;
      int intBid;
      if (!Int32.TryParse(userId, out intUserId) || !Int32.TryParse(bid, out intBid))
      {
        return null;
      }

      Table table = DataTemp.readData();
      DSeat currentSeat = table.seatsOrder.FirstOrDefault(o => o.Player.UserId == intUserId);
      table.Bid(intUserId, intBid);
      table.HighBid = currentSeat.RoundBid;

      table.PotValue += intBid;

      table.nextTurn = 1;
      var indexSeat = table.seatsOrder.IndexOf(currentSeat);
      List<DSeat> reorderedList = new List<DSeat>();

      reorderedList.AddRange(table.seatsOrder.GetRange(indexSeat, table.seatsOrder.Count - indexSeat));
      reorderedList.AddRange(table.seatsOrder.GetRange(0, indexSeat));
      table.seatsOrder = reorderedList;

      DataTemp.writeData(table);
      return "";
    }

    [HttpGet("{userId}")]
    public string Fold(string userId)
    {

      int intUserId;
      if (!Int32.TryParse(userId, out intUserId))
      {
        return null;
      }

      Table table = DataTemp.readData();
      DSeat currentSeat = table.seatsOrder.FirstOrDefault(o => o.Player.UserId == intUserId);
      currentSeat.Active = false;

      table.incrementTurn();

      DataTemp.writeData(table);
      return "";
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
