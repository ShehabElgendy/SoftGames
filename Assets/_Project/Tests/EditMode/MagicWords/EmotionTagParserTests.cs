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
        public void ParseEmotions_WithKnownTag_ReplacesWithSprite()
        {
            // Arrange
            string input = "I'm feeling {satisfied} today";

            // Act
            string result = EmotionTagParser.ParseEmotions(input);

            // Assert
            Assert.That(result, Does.Contain("<sprite"));
            Assert.That(result, Does.Not.Contain("{satisfied}"));
        }

        [Test]
        public void ParseEmotions_WithUnknownTag_RemovesTag()
        {
            // Arrange
            string input = "I'm feeling {unknownemotionxyz} today";

            // Act
            string result = EmotionTagParser.ParseEmotions(input);

            // Assert
            Assert.That(result, Does.Not.Contain("{unknownemotionxyz}"));
            Assert.That(result, Does.Not.Contain("<sprite"));
        }

        [Test]
        public void ParseEmotions_WithNoTags_ReturnsOriginal()
        {
            // Arrange
            string input = "Just a normal sentence";

            // Act
            string result = EmotionTagParser.ParseEmotions(input);

            // Assert
            Assert.AreEqual(input, result);
        }

        [Test]
        public void ParseEmotions_WithMultipleTags_ReplacesAll()
        {
            // Arrange
            string input = "I'm {satisfied} and {excited} about this";

            // Act
            string result = EmotionTagParser.ParseEmotions(input);

            // Assert
            Assert.That(result, Does.Not.Contain("{satisfied}"));
            Assert.That(result, Does.Not.Contain("{excited}"));
        }

        [Test]
        public void ParseEmotions_WithEmptyString_ReturnsEmpty()
        {
            // Arrange
            string input = "";

            // Act
            string result = EmotionTagParser.ParseEmotions(input);

            // Assert
            Assert.AreEqual("", result);
        }

        [Test]
        public void ParseEmotions_WithNullString_ReturnsEmpty()
        {
            // Arrange
            string input = null;

            // Act
            string result = EmotionTagParser.ParseEmotions(input);

            // Assert
            Assert.AreEqual("", result);
        }

        [Test]
        public void ParseEmotions_IsCaseInsensitive()
        {
            // Arrange
            string input1 = "{SATISFIED}";
            string input2 = "{Satisfied}";
            string input3 = "{satisfied}";

            // Act
            string result1 = EmotionTagParser.ParseEmotions(input1);
            string result2 = EmotionTagParser.ParseEmotions(input2);
            string result3 = EmotionTagParser.ParseEmotions(input3);

            // Assert - all should produce sprite tags
            Assert.That(result1, Does.Contain("<sprite"));
            Assert.That(result2, Does.Contain("<sprite"));
            Assert.That(result3, Does.Contain("<sprite"));
        }
    }
}
