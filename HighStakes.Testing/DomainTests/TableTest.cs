using System.Collections.Generic;
using HighStakes.Storing.Models;
using Xunit;

namespace HighStakes.Testing.DomainTests
{
    public class TableTest
    {
        [Fact]
        public void Test_JoinGame()
        {
            DTable table = new DTable();
            DUser user1 = new DUser();
            DUser user2 = new DUser();
            table.Initialize(5, 10);
            user1.ChipTotal = 100;
            user2.ChipTotal = 100;

            Assert.True(table.JoinGame(user1));
            Assert.True(table.JoinGame(user2));
            Assert.Equal(100, user1.ChipTotal);
        }

        [Fact]
        public void Test_NumOfActivePlayers()
        {
            DTable table = new DTable();
            DUser user1 = new DUser();
            DUser user2 = new DUser();
            table.Initialize(5, 10);
            user1.ChipTotal = 100;
            user2.ChipTotal = 100;

            table.JoinGame(user1);
            table.JoinGame(user2);
            
            Assert.Equal(2, table.NumOfActivePlayers());
        }

        [Fact]
        public void Test_StartGame()
        {
            DTable table = new DTable();
            DUser user1 = new DUser();
            DUser user2 = new DUser();
            table.Initialize(5, 10);
            user1.ChipTotal = 100;
            user2.ChipTotal = 100;

            table.JoinGame(user1);
            table.JoinGame(user2);

            table.StartGame();

            Assert.True(table.Seats[0].SmallBlind);
            Assert.True(table.Seats[1].BigBlind);
        }

        [Fact]
        public void Test_GetTurnOrder()
        {
            DTable table = new DTable();
            DUser user1 = new DUser();
            DUser user2 = new DUser();
            table.Initialize(5, 10);
            user1.ChipTotal = 100;
            user2.ChipTotal = 100;

            table.JoinGame(user1);
            table.JoinGame(user2);

            table.StartGame();
            table.GetTurnOrder();

            Assert.True(table.SeatsInTurnOrder[0].SmallBlind);
            Assert.True(table.SeatsInTurnOrder[1].BigBlind);
        }

       [Fact]
        public void Test_EndRound()
        {
            DTable table = new DTable();
            DUser user1 = new DUser();
            DUser user2 = new DUser();
            DUser user3 = new DUser();
            DCard flop1 = new DCard(0, 5, "Club");
            DCard flop2 = new DCard(0, 8, "Club");
            DCard flop3 = new DCard(0, 6, "Club");
            DCard flop4 = new DCard(0, 14, "Diamond");
            DCard flop5 = new DCard(0, 14, "Spade");
            DCard user11 = new DCard(0, 14, "Club");
            DCard user12 = new DCard(0, 14, "Heart");
            DCard user21 = new DCard(0, 3, "Club");
            DCard user22 = new DCard(0, 4, "Club");
            DCard user31 = new DCard(0, 13, "Club");
            DCard user32 = new DCard(0, 12, "Club");
            table.Initialize(5, 10);
            user1.ChipTotal = 100;
            user2.ChipTotal = 100;
            user3.ChipTotal = 100;

            table.JoinGame(user1);
            table.JoinGame(user2);
            table.JoinGame(user3);

            table.StartGame();
            table.GetTurnOrder();

            table.SeatsInTurnOrder[0].RoundBid = 15;
            table.SeatsInTurnOrder[1].RoundBid = 20;
            table.SeatsInTurnOrder[2].RoundBid = 20;

            foreach(DSeat seat in table.SeatsInTurnOrder)
            {
                seat.Flop.Add(flop1);
                seat.Flop.Add(flop2);
                seat.Flop.Add(flop3);
                seat.Flop.Add(flop4);
                seat.Flop.Add(flop5);
            }
            table.SeatsInTurnOrder[0].Pocket.Add(user11);
            table.SeatsInTurnOrder[0].Pocket.Add(user12);
            table.SeatsInTurnOrder[1].Pocket.Add(user21);
            table.SeatsInTurnOrder[1].Pocket.Add(user22);
            table.SeatsInTurnOrder[2].Pocket.Add(user31);
            table.SeatsInTurnOrder[2].Pocket.Add(user32);

            table.EndRound();

            Assert.Equal(145, table.SeatsInTurnOrder[0].ChipTotal);
            Assert.Equal(100, table.SeatsInTurnOrder[1].ChipTotal);
            Assert.Equal(110, table.SeatsInTurnOrder[2].ChipTotal);
            Assert.True(table.SeatsInTurnOrder[2].BigBlind);
        }

        [Fact]
        public void Test_MoveBlinds()
        {
            DTable table = new DTable();
            DUser user1 = new DUser();
            DUser user2 = new DUser();
            DUser user3 = new DUser();
            table.Initialize(5, 10);
            user1.ChipTotal = 100;
            user2.ChipTotal = 100;
            user3.ChipTotal = 100;

            table.JoinGame(user1);
            table.JoinGame(user2);
            table.JoinGame(user3);

            table.StartGame();
            table.GetTurnOrder();
            table.MoveBlinds();

            Assert.True(table.SeatsInTurnOrder[2].BigBlind);
            Assert.True(table.SeatsInTurnOrder[1].SmallBlind);
        }

        [Fact]
        public void Test_StartRound()
        {
            DTable table = new DTable();
            DUser user1 = new DUser();
            DUser user2 = new DUser();
            table.Initialize(5, 10);
            user1.ChipTotal = 100;
            user2.ChipTotal = 100;

            table.JoinGame(user1);
            table.JoinGame(user2);

            table.StartGame();
            table.StartRound();

            Assert.Equal(95, table.SeatsInTurnOrder[0].ChipTotal);
            Assert.Equal(90, table.SeatsInTurnOrder[1].ChipTotal);
            Assert.Equal(2, table.SeatsInTurnOrder[0].Pocket.Count);
        }
    }
}