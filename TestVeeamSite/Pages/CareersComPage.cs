using System;
using System.Threading;
using OpenQA.Selenium;

namespace TestVeeamSite.Pages
{
	public class CareersComPage : BasePage
	{
		const string URL = "https://careers.veeam.com/";
		
		const string applyFilterText = "Apply";
		const string jobsFoundText = "jobs found";

		readonly By countrySelectorParentLocator = By.Id("country-element");
		readonly By languageSelectorParentLocator = By.Id("language");
		readonly By vacanciesContainerLocator = By.ClassName("vacancies-blocks-container");
		
		public CareersComPage(IWebDriver driver) : base(driver) {}
		
		public override void GoToPage()
		{
			driver.Url = URL;
		}
		
		public void SelectCountry(string country)
		{
			var countryParent = driver.FindElement(countrySelectorParentLocator);
			var countrySpan = countryParent.FindElement(By.XPath(".//div/span"));
			countrySpan.Click();
			AdditionalWait(1);

			var countryElement = countryParent.FindElement(By.XPath($".//div/div/span[text()='{country}']"));
			countryElement.Click();
		}

		public void SelectLanguage(string language)
		{
			var languageParent = driver.FindElement(languageSelectorParentLocator);
			var languageSpan = languageParent.FindElement(By.XPath(".//span"));
			languageSpan.Click();
			AdditionalWait(1);
			
			var languageElement = languageParent.FindElement(By.XPath($".//label[contains(.,'{language}')]"));
			var languageElementSpan = languageElement.FindElement(By.XPath(".//span"));
			languageElementSpan.Click();
			
			var applyButton = languageParent.FindElement(By.XPath($".//a[text()='{applyFilterText}']"));
			applyButton.Click();
			AdditionalWait(10);
		}

		public int GetNumberOfJobs()
		{
			var vacanciesContainer = driver.FindElement(vacanciesContainerLocator);
			var numberOfJobsElement = vacanciesContainer.FindElement(By.XPath($"..//h3[contains(., '{jobsFoundText}')]"));
			return int.Parse(numberOfJobsElement.Text.Split(' ')[0]);
		}
	}
}