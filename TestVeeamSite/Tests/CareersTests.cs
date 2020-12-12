using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestVeeamSite.Pages;

namespace TestVeeamSite
{
	[TestFixture]
	public class CareersTests
	{
		const int IMPLICIT_WAIT_SECONDS = 10;
		
		IWebDriver driver;
		
		[SetUp]
		public void Setup()
		{
			driver = new OpenQA.Selenium.Chrome.ChromeDriver(Environment.CurrentDirectory + "\\..\\..\\drivers");
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(IMPLICIT_WAIT_SECONDS);
		}

		[TearDown]
		public void Clean()
		{
			driver.Quit();
		}

		[TestCase("Romania", "English", 21)]
		public void Test_NumberOfVacancies(string country, string language, int numberOfExpectedJobs)
		{
			try
			{
				CareersComPage careersComPage = new CareersComPage(driver);
				driver.Manage().Window.Maximize();
				careersComPage.GoToPage();
				careersComPage.AcknowledgeCookies();
				careersComPage.SelectCountry(country);
				careersComPage.SelectLanguage(language);
				var numberOfFoundJobs = careersComPage.GetNumberOfJobs();

				Assert.AreEqual(numberOfExpectedJobs, numberOfFoundJobs,
					$"Wrong number of found jobs. Expected is '{numberOfExpectedJobs}'. Actual is '{numberOfFoundJobs}'");
			}
			catch (Exception e)
			{
				throw;
			}
		}
	}
}