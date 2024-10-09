using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightPageObject;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
namespace PlaywrightTests;
[TestFixture]
class PlaywrightExample2 : PageTest
{
    [Test]
    public static async Task Main()
    {
        // Initialize Playwright
        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false 
        });

        // Create a new browser context and page
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        var pageObject = new PageObject(page);
        // Navigate to a URL
        await pageObject.NavigateURL();

        // Fill in all the information in form
        await pageObject.FillForm("TestCase2");

        // Click submit button
        await pageObject.ClickSubmitButton();

        // Verify not inputted field show up with red border and warning icon
        await pageObject.VerifyElementStyle();

        // Close the browser
        await browser.CloseAsync();
    }
}