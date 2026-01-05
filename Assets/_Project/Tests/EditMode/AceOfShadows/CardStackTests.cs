using NUnit.Framework;
using SoftGames.AceOfShadows;
using UnityEngine;

namespace SoftGames.Tests.EditMode.AceOfShadows
{
    /// <summary>
    /// Unit tests for CardStack logic.
    /// </summary>
    [TestFixture]
    public class CardStackTests
    {
        private GameObject _stackObject;
        private CardStack _cardStack;
        private Transform _container;

        [SetUp]
        public void SetUp()
        {
            _stackObject = new GameObject("TestStack");
            _cardStack = _stackObject.AddComponent<CardStack>();

            var containerObj = new GameObject("Container");
            containerObj.transform.SetParent(_stackObject.transform);
            _container = containerObj.transform;
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_stackObject);
        }

        [Test]
        public void CardCount_Initially_ReturnsZero()
        {
            Assert.AreEqual(0, _cardStack.CardCount);
        }

        [Test]
        public void AddCard_IncreasesCount()
        {
            // Arrange
            var cardObj = new GameObject("Card");
            var card = cardObj.AddComponent<CardController>();

            // Act
            _cardStack.AddCard(card);

            // Assert
            Assert.AreEqual(1, _cardStack.CardCount);

            // Cleanup
            Object.DestroyImmediate(cardObj);
        }

        [Test]
        public void RemoveTopCard_DecreasesCount()
        {
            // Arrange
            var cardObj = new GameObject("Card");
            var card = cardObj.AddComponent<CardController>();
            _cardStack.AddCard(card);

            // Act
            var removed = _cardStack.RemoveTopCard();

            // Assert
            Assert.AreEqual(0, _cardStack.CardCount);
            Assert.AreEqual(card, removed);

            // Cleanup
            Object.DestroyImmediate(cardObj);
        }

        [Test]
        public void RemoveTopCard_WhenEmpty_ReturnsNull()
        {
            // Act
            var result = _cardStack.RemoveTopCard();

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ClearCards_RemovesAll()
        {
            // Arrange
            for (int i = 0; i < 5; i++)
            {
                var cardObj = new GameObject($"Card{i}");
                var card = cardObj.AddComponent<CardController>();
                _cardStack.AddCard(card);
            }

            // Act
            _cardStack.ClearCards();

            // Assert
            Assert.AreEqual(0, _cardStack.CardCount);
        }

        [Test]
        public void GetNextSortingOrder_ReturnsExpectedValue()
        {
            // Arrange - add 5 cards
            for (int i = 0; i < 5; i++)
            {
                var cardObj = new GameObject($"Card{i}");
                var card = cardObj.AddComponent<CardController>();
                _cardStack.AddCard(card);
            }

            // Act
            int nextOrder = _cardStack.GetNextSortingOrder();

            // Assert - should be count (5)
            Assert.AreEqual(5, nextOrder);
        }
    }
}
