using System;
using System.Threading;
using OpenQA.Selenium;

namespace TestVeeamSite.Pages
{
	public class BasePage
	{
		const string URL = "https://veeam.com/";
		
		const string acknowledgeCookieButtonText = "OK, GOT IT!";

		protected readonly IWebDriver driver;

		readonly By cookieMessageLocator = By.ClassName("cookie-messaging");

		protected BasePage(IWebDriver driver)
		{
			this.driver = driver;
		}
		
		public virtual void GoToPage()
		{
			driver.Url = URL;
		}
		
		public virtual void AcknowledgeCookies()
		{
			var cookieMessage = driver.FindElement(cookieMessageLocator);
			var gotItButton = cookieMessage.FindElement(By.XPath($".//a[text()='{acknowledgeCookieButtonText}']"));
			gotItButton.Click();
		}

		protected static void AdditionalWait(int seconds)
		{
			Thread.Sleep(TimeSpan.FromSeconds(seconds));
		}
	}
}