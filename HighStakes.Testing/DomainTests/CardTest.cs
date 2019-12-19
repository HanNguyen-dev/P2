using HighStakes.Storing.Models;
using Xunit;

namespace HighStakes.Testing.DomainTests
{
    public class CardTest
    {
        [Theory]
        [InlineData(14, "Heart")]
        public void Test_Initialize(int value, string suit)
        {
            var card = new DCard(0, value, suit);

            Assert.True(card.Suit.Equals("Heart"));
            Assert.True(card.Value == 14);
        }
    }
}