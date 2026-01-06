using System.Collections.Generic;

namespace SoftGames.AceOfShadows
{
    /// <summary>
    /// Pure C# class for card stack logic.
    /// No Unity dependencies - fully testable in EditMode.
    /// </summary>
    public class CardStackModel<T> where T : class, ICard
    {
        private readonly List<T> cards = new();

        public int Count => cards.Count;

        public void Add(T card)
        {
            if (card != null)
            {
                cards.Add(card);
            }
        }

        public T RemoveTop()
        {
            if (cards.Count == 0) return null;

            var topCard = cards[^1];
            cards.RemoveAt(cards.Count - 1);
            return topCard;
        }

        public T Peek()
        {
            return cards.Count > 0 ? cards[^1] : null;
        }

        public void Clear()
        {
            cards.Clear();
        }

        public int GetNextSortingOrder()
        {
            return cards.Count;
        }
    }
}
