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
        public void SubscribeAndPublishReceivesEvent()
        {
            int receivedValue = 0;
            EventBus.Subscribe<TestEvent>(e => receivedValue = e.Value);

            EventBus.Publish(new TestEvent { Value = 42 });

            Assert.AreEqual(42, receivedValue);
        }

        [Test]
        public void UnsubscribeNoLongerReceivesEvent()
        {
            int receivedValue = 0;
            void Handler(TestEvent e) => receivedValue = e.Value;

            EventBus.Subscribe<TestEvent>(Handler);
            EventBus.Unsubscribe<TestEvent>(Handler);

            EventBus.Publish(new TestEvent { Value = 42 });

            Assert.AreEqual(0, receivedValue);
        }

        [Test]
        public void PublishWithNoSubscribersDoesNotThrow()
        {
            Assert.DoesNotThrow(() => EventBus.Publish(new TestEvent { Value = 42 }));
        }

        [Test]
        public void MultipleSubscribersAllReceiveEvent()
        {
            int count = 0;
            EventBus.Subscribe<TestEvent>(e => count++);
            EventBus.Subscribe<TestEvent>(e => count++);
            EventBus.Subscribe<TestEvent>(e => count++);

            EventBus.Publish(new TestEvent { Value = 1 });

            Assert.AreEqual(3, count);
        }

        [Test]
        public void ClearRemovesAllSubscribers()
        {
            int receivedValue = 0;
            EventBus.Subscribe<TestEvent>(e => receivedValue = e.Value);

            EventBus.Clear();
            EventBus.Publish(new TestEvent { Value = 42 });

            Assert.AreEqual(0, receivedValue);
        }

        [Test]
        public void ClearSpecificTypeOnlyRemovesThatType()
        {
            int testEventValue = 0;
            int otherEventValue = 0;

            EventBus.Subscribe<TestEvent>(e => testEventValue = e.Value);
            EventBus.Subscribe<SceneLoadedEvent>(e => otherEventValue = e.SceneIndex);

            EventBus.Clear<TestEvent>();
            EventBus.Publish(new TestEvent { Value = 42 });
            EventBus.Publish(new SceneLoadedEvent { SceneIndex = 99 });

            Assert.AreEqual(0, testEventValue);
            Assert.AreEqual(99, otherEventValue);
        }
    }
}
