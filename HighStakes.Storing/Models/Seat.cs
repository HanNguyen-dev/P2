using System.Collections.Generic;
using System.Linq;
using HighStakes.Storing.Abstracts;

namespace HighStakes.Storing.Models
{
    public class DSeat : AGame
    {
        public int SeatId { get; set; }
        public DUser Player { get; set; }
        public int ChipTotal { get; set; }
        public List<DCard> Pocket { get; set; }
        public DHand PlayerHand { get; set; }
        public bool BigBlind { get; set; }
        public bool SmallBlind { get; set; }
        public bool Occupied { get; set; }
        public int RoundBid { get; set; }
        public bool Active { get; set; }
        public int HandValue { get; set; }

        public void Initialize()
        {
            Player = new DUser();
            PlayerHand = new DHand();
            Pocket = new List<DCard>();
            Flop = new List<DCard>();
            PlayerHand.Initialize();
            Occupied = false;
            Active = false;
            RoundBid = 0;
            HandValue = 0;
        }

        public void NewRound()
        {
            Pocket.Clear();
            PlayerHand.HandCards.Clear();
            PlayerHand.HandValue = 0;
            Flop.Clear();
            Active = true;
            RoundBid = 0;
            HandValue = 0;
        }

        public void Bid(int bid)
        {
            if (bid > ChipTotal)
            {
                RoundBid += ChipTotal;
                ChipTotal = 0;
            } else {
                RoundBid += bid;
                ChipTotal -= bid;
            }
        }

        public void Fold()
        {
            Active = false;
        }

        public void SitDown(DUser player, int buyIn)
        {
            Player = player;
            ChipTotal = buyIn;
            Occupied = true;
            Active = true;
        }

        public void StandUp()
        {
            Player.ChipTotal += ChipTotal;
            Player = new DUser();
            ChipTotal = 0;
            Occupied = false;
            PlayerHand = new DHand();
            Pocket = new List<DCard>();
            Flop = new List<DCard>();
            PlayerHand.Initialize();
            SmallBlind = false;
            BigBlind = false;
            Active = false;
            RoundBid = 0;
            HandValue = 0;
        }

        public List<DCard> ReturnHand(int one, int two, int three, int four, int five, List<DCard> allCards)
        {
            List<DCard> returnHand = new List<DCard>();
            returnHand.Add(allCards[one]);
            returnHand.Add(allCards[two]);
            returnHand.Add(allCards[three]);
            returnHand.Add(allCards[four]);
            returnHand.Add(allCards[five]);

            return returnHand;
        }

        public void FindBestHand()
        {
            List<DCard> AllCards = new List<DCard>();
            List<DCard> BestHand = new List<DCard>();
            List<DCard> TempHand = new List<DCard>();
            int bestValue = 0;
            int tempValue = 0;

            foreach(DCard card in Flop)
            {
                AllCards.Add(card);
            }
            foreach(DCard card in Pocket)
            {
                AllCards.Add(card);
            }
            AllCards = AllCards.OrderByDescending(h => h.Value).ToList();

            BestHand = ReturnHand(0, 1, 2, 3, 4, AllCards);
            AssignHandValue(BestHand);
            bestValue = PlayerHand.HandValue;

            TempHand = ReturnHand(0, 1, 2, 3, 5, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 1, 2, 3, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 1, 2, 4, 5, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 1, 2, 4, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 1, 2, 5, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 1, 3, 4, 5, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 1, 3, 4, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 1, 3, 5, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 1, 4, 5, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 2, 3, 4, 5, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 2, 3, 4, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 2, 3, 5, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 2, 4, 5, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(0, 3, 4, 5, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(1, 2, 3, 4, 5, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(1, 2, 3, 4, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(1, 2, 3, 5, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(1, 2, 4, 5, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(1, 3, 4, 5, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
                bestValue = tempValue;
            }

            TempHand = ReturnHand(2, 3, 4, 5, 6, AllCards);
            AssignHandValue(TempHand);
            tempValue = PlayerHand.HandValue;

            if (tempValue > bestValue)
            {
                BestHand = new List<DCard>(TempHand);
            }
            AssignHandValue(BestHand);
            PlayerHand.HandCards = new List<DCard>(BestHand);
            HandValue = PlayerHand.HandValue;
        }

        public bool IsPair(List<DCard> hand)
        {
            return hand.GroupBy(h => h.Value).Count(g => g.Count() == 2) == 1;
        }

        public bool IsTwoPair(List<DCard> hand)
        {
            return hand.GroupBy(h => h.Value).Count(g => g.Count() == 2) == 2;
        }

        public bool IsThreeOfAKind(List<DCard> hand)
        {
            return hand.GroupBy(h => h.Value).Any(g => g.Count() == 3);
        }

        public bool IsFourOfAKind(List<DCard> hand)
        {
            return hand.GroupBy(h => h.Value).Any(g => g.Count() == 4);
        }

        public bool IsFlush(List<DCard> hand)
        {
            return hand.GroupBy(h => h.Suit).Any(g => g.Count() == 5);
        }

        public bool IsFullHouse(List<DCard> hand)
        {
            return IsPair(hand) && IsThreeOfAKind(hand);
        }

        public bool IsStraight(List<DCard> hand)
        {
            List<DCard> orderedHand = hand.OrderBy(h => h.Value).ToList();
            int curVal = 0;
            foreach(DCard card in orderedHand)
            {
                if (curVal == 0)
                {
                    curVal = card.Value;
                } else {
                    if (curVal != card.Value - 1)
                    {
                        return false;
                    }
                    curVal++;
                }
            }
            return true;
        }    

        public bool IsStraightFlush(List<DCard> hand)
        {
            return IsStraight(hand) && IsFlush(hand);
        }

        public bool IsRoyalStraightFlush(List<DCard> hand)
        {
            return IsStraightFlush(hand) && hand.Exists(c => c.Value == 14);
        }

        public void AssignHandValue(List<DCard> hand)
        {
            List<DCard> orderedHand = hand.OrderByDescending(h => h.Value).ToList();
            if (IsRoyalStraightFlush(hand))
            {
                PlayerHand.HandValue = 900;
            } else if (IsStraightFlush(hand))
            {
                PlayerHand.HandValue = 800;
                PlayerHand.HandValue += orderedHand[0].Value;
            } else if (IsFourOfAKind(hand))
            {
                PlayerHand.HandValue = 700;
                foreach(DCard card in orderedHand)
                {
                    PlayerHand.HandValue += card.Value;
                }
            } else if (IsFullHouse(hand))
            {
                PlayerHand.HandValue = 600;
                PlayerHand.HandValue += orderedHand[2].Value;

            } else if (IsFlush(hand))
            {
                PlayerHand.HandValue = 500;
                PlayerHand.HandValue += orderedHand[0].Value;
            } else if (IsStraight(hand))
            {
                PlayerHand.HandValue = 400;
                PlayerHand.HandValue += orderedHand[0].Value;
            } else if (IsThreeOfAKind(hand))
            {
                PlayerHand.HandValue = 300;
                PlayerHand.HandValue += orderedHand[2].Value;
            } else if (IsTwoPair(hand))
            {
                PlayerHand.HandValue = 200;
            } else if (IsPair(hand))
            {
                PlayerHand.HandValue = 100;
            } else 
            {
                PlayerHand.HandValue = orderedHand[0].Value;
            }
        }
    }
}