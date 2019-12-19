using System;
using System.Collections.Generic;
using HighStakes.Storing.Repositories;

namespace HighStakes.Storing.Models
{
    public class DDeck
    {
        public int DeckId { get; set; }
        public List<DCard> Cards { get; set; }

        public DDeck(int deckId, List<DCard> cards)
        {
            DeckId = deckId;
            Cards = cards;
        }
        public void Initialize()
        {
            Cards = new List<DCard>();
            DeckRepository dr = new DeckRepository(1);
            Cards = dr.GetDeck().Cards;
            Shuffle();
        }

        public void Shuffle()
        {
            Random rng = new Random();
            int count = Cards.Count;  
            while (count > 1) {  
                count--;  
                int rand = rng.Next(count + 1);  
                DCard card = Cards[rand];  
                Cards[rand] = Cards[count];  
                Cards[count] = card;  
            }  
        }

        public DCard Draw()
        {
            DCard draw = Cards[0];
            Cards.RemoveAt(0);
            return draw;
        }
    }
}