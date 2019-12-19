using HighStakes.Storing.Models;
using HighStakes.Storing.Adapters;

namespace HighStakes.Storing.Repositories
{
  public class DeckRepository
  {
 
    readonly DDeck _Deck;

    public DDeck GetDeck() { return _Deck; }
    public DeckRepository()
    {
         SqlAdapter sa = new SqlAdapter();
      _Deck = sa.getDeck();

    }
        public DeckRepository(int i)
    {
         SqlAdapter sa = new SqlAdapter(i);
      _Deck = sa.getDeck();

    }


  }




}
