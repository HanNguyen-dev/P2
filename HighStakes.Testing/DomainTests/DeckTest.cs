using System.Collections.Generic;
using HighStakes.Storing.Models;
using Xunit;

namespace HighStakes.Testing.DomainTests
{
    public class DeckTest
    {
        [Fact]
        public void Test_Initialize()
        {
            var deck = new DDeck(0, new List<DCard>());
            var expected = 52;

            deck.Initialize();

            Assert.Equal(expected, deck.Cards.Count);
        }

        [Fact]
        public void Test_Draw()
        {
            var deck = new DDeck(0, new List<DCard>());
            deck.Initialize();

            var card = deck.Draw();

            Assert.True(card.Value > 1);
        }

        [Fact]
        public void Test_Shuffle()
        {
            var deck = new DDeck(0, new List<DCard>());
            deck.Initialize();
            
            var card1 = deck.Cards[0];
            var card2 = deck.Cards[1];
            var card3 = deck.Cards[2];

            deck.Shuffle();

            Assert.False(card1.Equals(deck.Cards[0]) && card1.Equals(deck.Cards[1]) && card1.Equals(deck.Cards[2]));
        }
    }
}