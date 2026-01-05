using NUnit.Framework;
using SoftGames.PhoenixFlame;

namespace SoftGames.Tests.EditMode.PhoenixFlame
{
    /// <summary>
    /// Unit tests for ColorCycleModel (pure logic).
    /// No Unity dependencies.
    /// </summary>
    [TestFixture]
    public class ColorCycleModelTests
    {
        private ColorCycleModel model;

        [SetUp]
        public void SetUp()
        {
            model = new ColorCycleModel();
        }

        [Test]
        public void InitialIndexIsZero()
        {
            Assert.AreEqual(0, model.CurrentIndex);
        }

        [Test]
        public void InitialColorIsOrange()
        {
            Assert.AreEqual("Orange", model.CurrentColorName);
        }

        [Test]
        public void CycleNextMovesToGreen()
        {
            model.CycleNext();

            Assert.AreEqual(1, model.CurrentIndex);
            Assert.AreEqual("Green", model.CurrentColorName);
        }

        [Test]
        public void CycleNextTwiceMovesToBlue()
        {
            model.CycleNext();
            model.CycleNext();

            Assert.AreEqual(2, model.CurrentIndex);
            Assert.AreEqual("Blue", model.CurrentColorName);
        }

        [Test]
        public void CycleNextThreeTimesWrapsToOrange()
        {
            model.CycleNext();
            model.CycleNext();
            model.CycleNext();

            Assert.AreEqual(0, model.CurrentIndex);
            Assert.AreEqual("Orange", model.CurrentColorName);
        }

        [Test]
        public void CyclePreviousWrapsToBlue()
        {
            model.CyclePrevious();

            Assert.AreEqual(2, model.CurrentIndex);
            Assert.AreEqual("Blue", model.CurrentColorName);
        }

        [Test]
        public void SetIndexChangesColor()
        {
            model.SetIndex(2);

            Assert.AreEqual(2, model.CurrentIndex);
            Assert.AreEqual("Blue", model.CurrentColorName);
        }

        [Test]
        public void SetIndexWrapsOnOverflow()
        {
            model.SetIndex(5);

            Assert.AreEqual(2, model.CurrentIndex);
        }

        [Test]
        public void ResetGoesBackToOrange()
        {
            model.CycleNext();
            model.CycleNext();
            model.Reset();

            Assert.AreEqual(0, model.CurrentIndex);
            Assert.AreEqual("Orange", model.CurrentColorName);
        }

        [Test]
        public void ColorCountIsThree()
        {
            Assert.AreEqual(3, model.ColorCount);
        }

        [Test]
        public void ConstructorWithStartIndexWorks()
        {
            var customModel = new ColorCycleModel(1);

            Assert.AreEqual(1, customModel.CurrentIndex);
            Assert.AreEqual("Green", customModel.CurrentColorName);
        }
    }
}
