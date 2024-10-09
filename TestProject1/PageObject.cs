using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using TestProject1;

namespace PlaywrightPageObject;



public class PageObject
{
    
    #region Xpath
    string FormFields(string fieldName) => $"//input[@id='{fieldName}']";

    string RbGender (string option)=>$"//input[@name='gender'][@value='{option}']//following-sibling::label";

    string RbHobbie (string option)=>$"//input[@type='checkbox']/following-sibling::label[contains(text(),'{option}')]";

    string txbCurrentAddress => "//textarea[@placeholder='Current Address']";

    string StateCity(string fieldName) => $"//div[@id='{fieldName}']//descendant::input";

    string btnSubmit => $"//button[@id='submit']";

    string txtSubmitForm => "//div[contains(text(),\"Thanks for submitting the form\")]";

    string rowCount => "//tbody//tr";

    #endregion
    private readonly IPage _page;

    public PageObject(IPage page)
    {
        _page = page;
    }
    public async Task NavigateURL()
    {
        await _page.GotoAsync("https://demoqa.com/automation-practice-form");
    }    

    public async Task ClickSubmitButton()
    {
        await _page.ClickAsync(btnSubmit);
    }

    public async Task FillForm(string data)
    {
        var value = Constants.TestCase1;
        if (data == "TestCase2")
        {
            value = Constants.TestCase2;
        }

        foreach (var kvp in value)
        {
            await _page.WaitForTimeoutAsync(500);
            switch (kvp.Key)
            {
                case "firstName":
                case "lastName":
                case "userNumber":
                case "userEmail":
                    {
                        await FillField(kvp.Key, kvp.Value);
                        break;
                    }
                case "gender":
                    {
                        await _page.CheckAsync(RbGender(kvp.Value));
                        break;
                    }
                case "dateOfBirthInput":
                case "subjectsInput":
                    {
                        await FillField(kvp.Key, kvp.Value);
                        await _page.PressAsync(FormFields(kvp.Key), "Enter");
                        break;
                    }

                case "hobbies":
                    {
                        await _page.CheckAsync(RbHobbie(kvp.Value));
                        break;
                    }
                case "uploadPicture":
                    {
                        await _page.SetInputFilesAsync(FormFields(kvp.Key), "C:\\Users\\Dell\\source\\repos\\TestProject1\\TestProject1\\uploadPic.jfif");
                        break;
                    }
                case "currentAddress":
                    {
                        await _page.FillAsync(txbCurrentAddress,kvp.Value);
                        break;
                    }
                case "state":
                case "city":
                    {                       
                        await _page.FillAsync(StateCity(kvp.Key),kvp.Value);
                        await _page.PressAsync(StateCity(kvp.Key), "Enter");
                        break;
                    }
                
            }
        }

    }

    public async Task IsTextAppear()
    {
        await _page.IsVisibleAsync(txtSubmitForm);
        await _page.WaitForTimeoutAsync(1000);

    }

    public async Task IsDataCorrect()
    {
        int row = await _page.Locator(rowCount).CountAsync();
        int i = 1;
        List<string> textsFromNthRow = new List<string>();
        while (i <= row)
        {
            textsFromNthRow.Add(await _page.InnerTextAsync($"//tbody//tr[{i}]"));
            i++;
        }
        foreach (var kvp in Constants.TestCase1)
        {
            switch (kvp.Key)
            {
                case "firstName":
                case "lastName":
                    {
                        Assert.That(textsFromNthRow[0], Does.Contain(kvp.Value).IgnoreCase);
                        break;
                    }
                case "userNumber":
                    {
                        Assert.That(textsFromNthRow[3], Does.Contain(kvp.Value).IgnoreCase);
                        break;
                    }
                case "userEmail":
                    {
                        Assert.That(textsFromNthRow[1], Does.Contain(kvp.Value).IgnoreCase);
                        break;
                    }
                case "gender":
                    {
                        Assert.That(textsFromNthRow[2], Does.Contain(kvp.Value).IgnoreCase);
                        break;
                    }
                case "dateOfBirthInput":
                    {
                        Assert.That(textsFromNthRow[4], Does.Contain(kvp.Value).IgnoreCase);
                        break;
                    }
                case "subjectsInput":
                    {
                        Assert.That(textsFromNthRow[5], Does.Contain(kvp.Value).IgnoreCase);
                        break;
                    }

                case "hobbies":
                    {
                        Assert.That(textsFromNthRow[6], Does.Contain(kvp.Value).IgnoreCase);
                        break;
                    }
                case "uploadPicture":
                    {
                        Assert.That(textsFromNthRow[7], Does.Contain(kvp.Value).IgnoreCase);
                        break;
                    }
                case "currentAddress":
                    {
                        Assert.That(textsFromNthRow[8], Does.Contain(kvp.Value).IgnoreCase);
                        break;
                    }
                case "state":
                case "city":
                    {
                        Assert.That(textsFromNthRow[9], Does.Contain(kvp.Value).IgnoreCase);
                        break;
                    }
            }
        }

    }
    public async Task FillField(string fieldName, string value)
    {
        await _page.IsVisibleAsync(FormFields(fieldName));
        await _page.FillAsync(FormFields(fieldName), value);
    }

    public async Task VerifyElementStyle()
    {
        // Select the element         
        var element = await _page.QuerySelectorAsync(FormFields("userNumber"));
        await element.ScrollIntoViewIfNeededAsync();
        await _page.WaitForTimeoutAsync(2000);

        // Get the computed border color
        var borderColor = await _page.EvaluateAsync<string>(
            "(element) => getComputedStyle(element).borderColor", element);

        // Get the computed background image
        var backgroundImage = await _page.EvaluateAsync<string>(
            "(element) => getComputedStyle(element).backgroundImage", element);

        // Example assertion: Check if the styles are as expected
        Assert.That(borderColor, Is.EqualTo("rgb(220, 53, 69)"));
        Assert.That(backgroundImage, Is.EqualTo("url(\"data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' width='12' height='12' fill='none' stroke='%23dc3545' viewBox='0 0 12 12'%3e%3ccircle cx='6' cy='6' r='4.5'/%3e%3cpath stroke-linejoin='round' d='M5.8 3.6h.4L6 6.5z'/%3e%3ccircle cx='6' cy='8.2' r='.6' fill='%23dc3545' stroke='none'/%3e%3c/svg%3e\")"));
        await _page.WaitForTimeoutAsync(2000);
    }
}