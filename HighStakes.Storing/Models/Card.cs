namespace HighStakes.Storing.Models
{
    public class DCard
    {
        public int CardId { get; set; }
        public int Value { get; set; }
        public string Suit { get; set; }

        public DCard(int cardId, int value, string suit)
        {
            CardId = cardId;
            Value = value;
            Suit = suit;
        }
    }
}