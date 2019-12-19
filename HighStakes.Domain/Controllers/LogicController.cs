using System;
using System.Threading.Tasks;
using HighStakes.Storing.Models;
using HighStakes.Storing.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HighStakes.Domain.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class LogicController : ControllerBase
    {
        private static UserRepository _uRepo = new UserRepository();

        [HttpGet("{username}/{password}")]
        public async Task<string> Login(string username, string password)
        {
            var user = _uRepo.GetUser(username, password);
            string returnVal = JsonConvert.SerializeObject(user);
            return await Task.Run(() => { return returnVal;});
        }


        [HttpGet]
        public async Task<string> GetTable()
        {
            DTable table = new DTable();
            table.Initialize(10, 20);
            string returnVal = JsonConvert.SerializeObject(table);
            return await Task.Run(() => { return returnVal;});
        }

        [HttpGet("{jsonTable}/{jsonUser}")]
        public async Task<string> JoinTable(string jsonTable, string jsonUser)
        {
            DTable table = JsonConvert.DeserializeObject<DTable>(jsonTable);
            DUser player = JsonConvert.DeserializeObject<DUser>(jsonUser);

            table.JoinGame(player);

            string returnVal = JsonConvert.SerializeObject(table);
            return await Task.Run(() => { return returnVal;});
        }

        [HttpGet("{jsonTable}")]
        public async Task<IActionResult> StartGame(string jsonTable)
        {
            DTable table = JsonConvert.DeserializeObject<DTable>(jsonTable);

            table.StartGame();

            string returnVal = JsonConvert.SerializeObject(table);
            return await Task.Run(() => { return Ok(returnVal);});
        }

        [HttpGet("{jsonTable}")]
        public async Task<IActionResult> StartRound(string jsonTable)
        {
            DTable table = JsonConvert.DeserializeObject<DTable>(jsonTable);

            table.StartRound();

            for (int i = 0; i < 5; i++)
            {
              table.Flop.Add(table.DeckOfCards.Draw());
            }

            string returnVal = JsonConvert.SerializeObject(table);
            return await Task.Run(() => { return Ok(returnVal);});
        }

        [HttpGet("{jsonSeat}/{bid}")]
        public async Task<string> Bid(string jsonSeat, string bid)
        {
            DSeat seat = JsonConvert.DeserializeObject<DSeat>(jsonSeat);
            int actualBid;

            if (!Int32.TryParse(bid, out actualBid))
            {
                return null;
            }

            seat.Bid(actualBid);

            string returnVal = JsonConvert.SerializeObject(seat);
            return await Task.Run(() => { return returnVal;});
        }

        public async Task<IActionResult> EndRound(string jsonTable)
        {
            DTable table = JsonConvert.DeserializeObject<DTable>(jsonTable);

            table.EndRound();

            string returnVal = JsonConvert.SerializeObject(table);
            return await Task.Run(() => { return Ok(returnVal);});
        }

    }
}