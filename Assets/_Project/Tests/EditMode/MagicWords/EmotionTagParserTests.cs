using NUnit.Framework;
using SoftGames.MagicWords;

namespace SoftGames.Tests.EditMode.MagicWords
{
    /// <summary>
    /// Unit tests for EmotionTagParser.
    /// </summary>
    [TestFixture]
    public class EmotionTagParserTests
    {
        [Test]
        public void ParseEmotionsWithKnownTagReplacesWithSprite()
        {
            string input = "I'm feeling {satisfied} today";

            string result = EmotionTagParser.ParseEmotions(input);

            Assert.That(result, Does.Contain("<sprite"));
            Assert.That(result, Does.Not.Contain("{satisfied}"));
        }

        [Test]
        public void ParseEmotionsWithUnknownTagRemovesTag()
        {
            string input = "I'm feeling {unknownemotionxyz} today";

            string result = EmotionTagParser.ParseEmotions(input);

            Assert.That(result, Does.Not.Contain("{unknownemotionxyz}"));
            Assert.That(result, Does.Not.Contain("<sprite"));
        }

        [Test]
        public void ParseEmotionsWithNoTagsReturnsOriginal()
        {
            string input = "Just a normal sentence";

            string result = EmotionTagParser.ParseEmotions(input);

            Assert.AreEqual(input, result);
        }

        [Test]
        public void ParseEmotionsWithMultipleTagsReplacesAll()
        {
            string input = "I'm {satisfied} and {excited} about this";

            string result = EmotionTagParser.ParseEmotions(input);

            Assert.That(result, Does.Not.Contain("{satisfied}"));
            Assert.That(result, Does.Not.Contain("{excited}"));
        }

        [Test]
        public void ParseEmotionsWithEmptyStringReturnsEmpty()
        {
            string input = "";

            string result = EmotionTagParser.ParseEmotions(input);

            Assert.AreEqual("", result);
        }

        [Test]
        public void ParseEmotionsWithNullStringReturnsEmpty()
        {
            string input = null;

            string result = EmotionTagParser.ParseEmotions(input);

            Assert.AreEqual("", result);
        }

        [Test]
        public void ParseEmotionsIsCaseInsensitive()
        {
            string input1 = "{SATISFIED}";
            string input2 = "{Satisfied}";
            string input3 = "{satisfied}";

            string result1 = EmotionTagParser.ParseEmotions(input1);
            string result2 = EmotionTagParser.ParseEmotions(input2);
            string result3 = EmotionTagParser.ParseEmotions(input3);

            Assert.That(result1, Does.Contain("<sprite"));
            Assert.That(result2, Does.Contain("<sprite"));
            Assert.That(result3, Does.Contain("<sprite"));
        }
    }
}
