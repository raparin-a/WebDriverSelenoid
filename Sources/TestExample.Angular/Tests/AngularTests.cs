using NUnit.Framework;
using SeleniumWebDriver.Configuration;
using SeleniumWebDriver.Elements.Factories;

namespace TestExample.Angular.Tests
{
    [TestFixture]
    public class AngularTests : TestsBase
    {
        [Test]
        public void OpenPage_TitleIsCorrect()
        {
            // Arrange
            Settings.ConfigureWebDriver();
            var mainPage = PageFactory.Get<Pages.Angular>();
            
            // Act
            mainPage.Open();
            mainPage.GetStartedButton.Click();

            // Assert
            Assert.That(mainPage.IntroductionTitle.Text, Does.Contain("Introduction to the Angular Docs"));
        }

        [Test]
        public void OpenPage_TitleIsIncorrect()
        {
            // Arrange
            Settings.ConfigureWebDriver();
            var mainPage = PageFactory.Get<Pages.Angular>();
            
            // Act
            mainPage.Open();
            mainPage.GetStartedButton.Click();

            // Assert
            Assert.That(mainPage.IntroductionTitle.Text, Does.Not.Contain("Introduction to the Angular Docs"));
        }
    }
}
