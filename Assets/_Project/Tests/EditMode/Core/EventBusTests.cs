using NUnit.Framework;
using SoftGames.Core;

namespace SoftGames.Tests.EditMode.Core
{
    /// <summary>
    /// Unit tests for EventBus.
    /// </summary>
    [TestFixture]
    public class EventBusTests
    {
        private struct TestEvent : IEvent
        {
            public int Value;
        }

        [SetUp]
        public void SetUp()
        {
            EventBus.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            EventBus.Clear();
        }

        [Test]
        public void Subscribe_And_Publish_ReceivesEvent()
        {
            // Arrange
            int receivedValue = 0;
            EventBus.Subscribe<TestEvent>(e => receivedValue = e.Value);

            // Act
            EventBus.Publish(new TestEvent { Value = 42 });

            // Assert
            Assert.AreEqual(42, receivedValue);
        }

        [Test]
        public void Unsubscribe_NoLongerReceivesEvent()
        {
            // Arrange
            int receivedValue = 0;
            void Handler(TestEvent e) => receivedValue = e.Value;

            EventBus.Subscribe<TestEvent>(Handler);
            EventBus.Unsubscribe<TestEvent>(Handler);

            // Act
            EventBus.Publish(new TestEvent { Value = 42 });

            // Assert
            Assert.AreEqual(0, receivedValue);
        }

        [Test]
        public void Publish_WithNoSubscribers_DoesNotThrow()
        {
            // Act & Assert - should not throw
            Assert.DoesNotThrow(() => EventBus.Publish(new TestEvent { Value = 42 }));
        }

        [Test]
        public void MultipleSubscribers_AllReceiveEvent()
        {
            // Arrange
            int count = 0;
            EventBus.Subscribe<TestEvent>(_ => count++);
            EventBus.Subscribe<TestEvent>(_ => count++);
            EventBus.Subscribe<TestEvent>(_ => count++);

            // Act
            EventBus.Publish(new TestEvent { Value = 1 });

            // Assert
            Assert.AreEqual(3, count);
        }

        [Test]
        public void Clear_RemovesAllSubscribers()
        {
            // Arrange
            int receivedValue = 0;
            EventBus.Subscribe<TestEvent>(e => receivedValue = e.Value);

            // Act
            EventBus.Clear();
            EventBus.Publish(new TestEvent { Value = 42 });

            // Assert
            Assert.AreEqual(0, receivedValue);
        }

        [Test]
        public void ClearSpecificType_OnlyRemovesThatType()
        {
            // Arrange
            int testEventValue = 0;
            int otherEventValue = 0;

            EventBus.Subscribe<TestEvent>(e => testEventValue = e.Value);
            EventBus.Subscribe<SceneLoadedEvent>(e => otherEventValue = e.SceneIndex);

            // Act
            EventBus.Clear<TestEvent>();
            EventBus.Publish(new TestEvent { Value = 42 });
            EventBus.Publish(new SceneLoadedEvent { SceneIndex = 99 });

            // Assert
            Assert.AreEqual(0, testEventValue);
            Assert.AreEqual(99, otherEventValue);
        }
    }
}
