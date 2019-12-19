using System.Collections.Generic;
using System.Linq;
using HighStakes.Storing.Abstracts;

namespace HighStakes.Storing.Models
{
    public class DTable : AGame
    {
        public int TableId { get; set; }
        public List<DSeat> Seats { get; set; }
        public DDeck DeckOfCards { get; set; }
        public int SmallBlindAmount { get; set; }
        public int BigBlindAmount { get; set; }
        public List<DSeat> SeatsInTurnOrder { get; set; }
        public bool RoundStarted { get; set; }

        public void Initialize(int smallBlindAmount, int bigBlindAmount)
        {
            Seats = new List<DSeat>();
            Flop = new List<DCard>();
            DeckOfCards = new DDeck(0, new List<DCard>());
            DeckOfCards.Initialize();
            SeatsInTurnOrder = new List<DSeat>();
            SmallBlindAmount = smallBlindAmount;
            BigBlindAmount = bigBlindAmount;
            RoundStarted = false;
            for (int i = 0; i < 6; i++)
            {
                DSeat seat = new DSeat();
                seat.Initialize();
                Seats.Add(seat);
            }
        }

        public bool JoinGame(DUser player)
        {
            foreach(DSeat seat in Seats)
            {
                if (!seat.Occupied)
                {
                    seat.SitDown(player, player.ChipTotal);
                    player.ChipTotal -= 0;
                    if (RoundStarted)
                    {
                        seat.Active = false;
                    }
                    return true;
                }
            }
            return false;
        }

        public int NumOfActivePlayers()
        {
            int NumberOfPlayers = 0;
            foreach(DSeat seat in Seats)
            {
                if (seat.Occupied)
                    NumberOfPlayers++;
            }
            return NumberOfPlayers;
        }

        public bool StartGame()
        {
            int NumberOfPlayers = 0;
            bool AssignSmallBlind = false;
            bool AssignBigBlind = false;
            int i = 0;

            NumberOfPlayers = NumOfActivePlayers();

            if (NumberOfPlayers >= 2)
            {
                for (i = 0; i < 6; i++)
                {
                    if (Seats[i].Occupied)
                    {
                        if (!AssignSmallBlind)
                        {
                            Seats[i].SmallBlind = true;
                            AssignSmallBlind = true;
                        } else if (!AssignBigBlind)
                        {
                            Seats[i].BigBlind = true;
                            AssignBigBlind = true;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public void GetTurnOrder()
        {
            int startIndex = 0;
            SeatsInTurnOrder.Clear();
            foreach(DSeat seat in Seats)
            {
                if (seat.SmallBlind)
                {
                    startIndex = Seats.IndexOf(seat);
                }
            }
            for (int i = 0; i < 6; i++)
            {
                if (startIndex >= 6)
                {
                    startIndex = 0;
                }
                if (Seats[startIndex].Occupied)
                {
                    SeatsInTurnOrder.Add(Seats[startIndex]);
                }
                startIndex++;
            }
        }

        public void EndRound()
        {
            int PotTotal = 0;
            int NewPot = 0;
            List<DSeat> PeopleWhoCanWinMoney = new List<DSeat>();
            List<DSeat> NewPotPeople = new List<DSeat>();
            List<DSeat> MatchingHandValues = new List<DSeat>();
            GetTurnOrder();
            foreach (DSeat seat in SeatsInTurnOrder)
            {
                if (seat.Occupied && seat.Active && seat.RoundBid > 0)
                {
                    PeopleWhoCanWinMoney.Add(seat);
                }
            }
            foreach (DSeat seat in SeatsInTurnOrder)
            {
                if (seat.Occupied)
                {
                    PotTotal += seat.RoundBid;
                }
            }
            if (PeopleWhoCanWinMoney.Count == 1)
            {
                PeopleWhoCanWinMoney[0].ChipTotal += PotTotal;
                MoveBlinds();
                Flop.Clear();
                return;
            }
            foreach (DSeat seat in PeopleWhoCanWinMoney)
            {
                seat.FindBestHand();
            }
            do {
                PeopleWhoCanWinMoney = PeopleWhoCanWinMoney.OrderByDescending(h => h.HandValue).ToList();
                if (PeopleWhoCanWinMoney[0].HandValue > PeopleWhoCanWinMoney[1].HandValue)
                {
                    NewPotPeople.Clear();
                    for (int i = 1; i < PeopleWhoCanWinMoney.Count; i++)
                    {
                        if (PeopleWhoCanWinMoney[i].RoundBid > PeopleWhoCanWinMoney[0].RoundBid)
                        {
                            NewPotPeople.Add(PeopleWhoCanWinMoney[i]);
                        }
                    }
                    if (NewPotPeople.Count == 0)
                    {
                        PeopleWhoCanWinMoney[0].ChipTotal += PotTotal;
                        MoveBlinds();
                        Flop.Clear();
                        return;
                    } else {
                        foreach (DSeat seat in NewPotPeople)
                        {
                            NewPot += seat.RoundBid - PeopleWhoCanWinMoney[0].RoundBid;
                            seat.RoundBid -= PeopleWhoCanWinMoney[0].RoundBid;
                        }
                        PotTotal -= NewPot;
                        PeopleWhoCanWinMoney[0].ChipTotal += PotTotal;
                        PeopleWhoCanWinMoney = new List<DSeat>(NewPotPeople);
                        PotTotal = NewPot;
                        NewPot = 0;
                    }
                } else {
                    //get list of people who have matching hand values
                    MatchingHandValues.Clear();
                    MatchingHandValues.Add(PeopleWhoCanWinMoney[0]);
                    for (int i = 1; i < PeopleWhoCanWinMoney.Count; i++)
                    {
                        if (PeopleWhoCanWinMoney[i].HandValue == PeopleWhoCanWinMoney[0].HandValue)
                        {
                            MatchingHandValues.Add(PeopleWhoCanWinMoney[i]);
                        }
                    }
                    //run through the cards one by one to see if someone has a high value
                    for (int i = 0; i < 5; i++)
                    {
                        foreach (DSeat seat in MatchingHandValues)
                        {
                            seat.HandValue += seat.PlayerHand.HandCards[i].Value;
                        }
                        MatchingHandValues = MatchingHandValues.OrderByDescending(h => h.HandValue).ToList();
                        if (MatchingHandValues[0].HandValue > MatchingHandValues[1].HandValue)
                        {
                            break;
                        }
                    }
                    //if someone has the highest value
                    if (MatchingHandValues[0].HandValue > MatchingHandValues[1].HandValue)
                    {
                        PeopleWhoCanWinMoney = PeopleWhoCanWinMoney.OrderByDescending(h => h.HandValue).ToList();
                        NewPotPeople.Clear();
                        for (int i = 1; i < PeopleWhoCanWinMoney.Count; i++)
                        {
                            if (PeopleWhoCanWinMoney[i].RoundBid > PeopleWhoCanWinMoney[0].RoundBid)
                            {
                                NewPotPeople.Add(PeopleWhoCanWinMoney[i]);
                            }
                        }
                        if (NewPotPeople.Count == 0)
                        {
                            PeopleWhoCanWinMoney[0].ChipTotal += PotTotal;
                        } else {
                            foreach (DSeat seat in NewPotPeople)
                            {
                                NewPot += seat.RoundBid - PeopleWhoCanWinMoney[0].RoundBid;
                                seat.RoundBid -= PeopleWhoCanWinMoney[0].RoundBid;
                            }
                            PotTotal -= NewPot;
                            PeopleWhoCanWinMoney[0].ChipTotal += PotTotal;
                            PeopleWhoCanWinMoney = new List<DSeat>(NewPotPeople);
                            PotTotal = NewPot;
                            NewPot = 0;
                            foreach (DSeat seat in PeopleWhoCanWinMoney)
                            {
                                seat.FindBestHand();
                            }
                        }
                    } else {
                        // may have matching hand levels but different pot levels\
                        MatchingHandValues = MatchingHandValues.OrderByDescending(h => h.HandValue).ToList();
                        List<DSeat> StillMatching = new List<DSeat>();
                        StillMatching.Add(MatchingHandValues[0]);

                        for (int i = 1; i < MatchingHandValues.Count; i++)
                        {
                            if (MatchingHandValues[i].HandValue == StillMatching[0].HandValue)
                            {
                                StillMatching.Add(MatchingHandValues[0]);
                            }
                        }
                        StillMatching = StillMatching.OrderBy(h => h.RoundBid).ToList();

                        NewPotPeople.Clear();
                        for (int i = 1; i < PeopleWhoCanWinMoney.Count; i++)
                        {
                            if (PeopleWhoCanWinMoney[i].RoundBid > StillMatching[0].RoundBid)
                            {
                                NewPotPeople.Add(PeopleWhoCanWinMoney[i]);
                            }
                        }
                        if (NewPotPeople.Count == 0)
                        {
                            // divide pot among still matching
                            if (PotTotal % StillMatching.Count != 0)
                            {
                                StillMatching[0].ChipTotal += 1;
                            }
                            foreach (DSeat seat in StillMatching)
                            {
                                seat.ChipTotal += PotTotal / StillMatching.Count;
                            }
                        } else {

                            foreach (DSeat seat in NewPotPeople)
                            {
                                NewPot += seat.RoundBid - StillMatching[0].RoundBid;     
                                seat.RoundBid -= StillMatching[0].RoundBid;
                            }
                            PotTotal -= NewPot;
                            foreach (DSeat seat in StillMatching)
                            {
                                seat.ChipTotal += PotTotal / StillMatching.Count;
                            }
                            PeopleWhoCanWinMoney = new List<DSeat>(NewPotPeople);
                            PotTotal = NewPot;
                            NewPot = 0;
                        }
                    }
                }
            } while (NewPotPeople.Count > 0);  
            // NEED TO ADD MOVING THE SMALL BLIND BIG BLIND
            MoveBlinds();
            Flop.Clear();
        }

        public void MoveBlinds()
        {
            GetTurnOrder();
            if (SeatsInTurnOrder.Count > 2)
            {
                SeatsInTurnOrder[0].SmallBlind = false;
                SeatsInTurnOrder[1].SmallBlind = true;
                SeatsInTurnOrder[1].BigBlind = false;
                SeatsInTurnOrder[2].BigBlind = true;
            } else if (SeatsInTurnOrder.Count == 2)
            {
                SeatsInTurnOrder[0].BigBlind = true;
                SeatsInTurnOrder[0].SmallBlind = false;
                SeatsInTurnOrder[1].BigBlind = false;
                SeatsInTurnOrder[1].SmallBlind = true;
            }
        }

        public void StartRound()
        {
            DeckOfCards.Initialize();

            foreach (DSeat seat in Seats)
            {
                if (seat.SmallBlind)
                {
                    seat.Bid(SmallBlindAmount);
                }
                if (seat.BigBlind)
                {
                    seat.Bid(BigBlindAmount);
                }
            }

            GetTurnOrder();

            foreach(DSeat seat in SeatsInTurnOrder)
            {
                seat.NewRound();
            }

            for (int i = 0; i < 2; i++)
            {
                foreach(DSeat seat in SeatsInTurnOrder)
                {
                    seat.Pocket.Add(DeckOfCards.Draw());
                }
            }
        }
    }
}