using NUnit.Framework;
using SoftGames.AceOfShadows;
using UnityEngine;

namespace SoftGames.Tests.EditMode.AceOfShadows
{
    /// <summary>
    /// Mock card for testing - no Unity dependencies.
    /// </summary>
    public class MockCard : ICard
    {
        public Vector2 LastPosition { get; private set; }
        public int LastSortingOrder { get; private set; }
        public bool IsAnimating { get; private set; }

        public void SetStackPosition(Vector2 position) => LastPosition = position;
        public void SetSortingOrder(int order) => LastSortingOrder = order;
        public void SetAnimating(bool animating) => IsAnimating = animating;
    }

    /// <summary>
    /// Unit tests for CardStackModel (pure logic).
    /// No Unity MonoBehaviour dependencies.
    /// </summary>
    [TestFixture]
    public class CardStackModelTests
    {
        private CardStackModel<MockCard> model;

        [SetUp]
        public void SetUp()
        {
            model = new CardStackModel<MockCard>();
        }

        [Test]
        public void CountInitiallyReturnsZero()
        {
            Assert.AreEqual(0, model.Count);
        }

        [Test]
        public void AddWithNullDoesNotIncreaseCount()
        {
            model.Add(null);
            Assert.AreEqual(0, model.Count);
        }

        [Test]
        public void AddWithValidCardIncreasesCount()
        {
            model.Add(new MockCard());
            Assert.AreEqual(1, model.Count);
        }

        [Test]
        public void AddMultipleCardsIncreasesCountCorrectly()
        {
            model.Add(new MockCard());
            model.Add(new MockCard());
            model.Add(new MockCard());
            Assert.AreEqual(3, model.Count);
        }

        [Test]
        public void RemoveTopWhenEmptyReturnsNull()
        {
            var result = model.RemoveTop();
            Assert.IsNull(result);
        }

        [Test]
        public void RemoveTopReturnsLastAddedCard()
        {
            var card1 = new MockCard();
            var card2 = new MockCard();
            model.Add(card1);
            model.Add(card2);

            var result = model.RemoveTop();

            Assert.AreEqual(card2, result);
        }

        [Test]
        public void RemoveTopDecreasesCount()
        {
            model.Add(new MockCard());
            model.Add(new MockCard());
            model.RemoveTop();

            Assert.AreEqual(1, model.Count);
        }

        [Test]
        public void PeekWhenEmptyReturnsNull()
        {
            var result = model.Peek();
            Assert.IsNull(result);
        }

        [Test]
        public void PeekReturnsTopCardWithoutRemoving()
        {
            var card = new MockCard();
            model.Add(card);

            var result = model.Peek();

            Assert.AreEqual(card, result);
            Assert.AreEqual(1, model.Count);
        }

        [Test]
        public void ClearRemovesAllCards()
        {
            model.Add(new MockCard());
            model.Add(new MockCard());
            model.Add(new MockCard());

            model.Clear();

            Assert.AreEqual(0, model.Count);
        }

        [Test]
        public void GetNextSortingOrderReturnsCount()
        {
            Assert.AreEqual(0, model.GetNextSortingOrder());

            model.Add(new MockCard());
            Assert.AreEqual(1, model.GetNextSortingOrder());

            model.Add(new MockCard());
            Assert.AreEqual(2, model.GetNextSortingOrder());
        }
    }
}
