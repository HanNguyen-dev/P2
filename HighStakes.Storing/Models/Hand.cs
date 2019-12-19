using System.Collections.Generic;

namespace HighStakes.Storing.Models
{
    public class DHand
    {
        public int HandId { get; set; }
        public List<DCard> HandCards { get; set; }
        public int HandValue { get; set; }

        public void Initialize()
        {
            HandCards = new List<DCard>();
        }
    }
}