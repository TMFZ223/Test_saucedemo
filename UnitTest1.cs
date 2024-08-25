using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

public class TestSaucedemo
{
    [Fact]
    // ���� �� �����������
    public void TestCheckAuthorization()
    {
        string url = "https://www.saucedemo.com/";

        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(url);

            // ������� ���� �����������
            var login = driver.FindElement(By.XPath("//input[@placeholder='Username']"));
            var password = driver.FindElement(By.XPath("//input[@placeholder='Password']"));
            // ��������� ���� �����������
            login.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            // ������� ������ �����
            var loginButton = driver.FindElement(By.XPath("//input[@value='Login']"));
            // ������� �� ������ �����
            loginButton.Click();
            string expectedResult = "https://www.saucedemo.com/inventory.html"; // ��������� ����� �������� ����� �����������
            var actualResult = driver.Url; // ����������� ���������
            Assert.Equal(expectedResult, actualResult);
        }
    }
    [Fact]
    // ���� �� ���������� ������� � �������
    public void TestCheckAddProductCart()
    {
        string url = "https://www.saucedemo.com/";

        using (IWebDriver driver = new ChromeDriver()) // �������� ���������� Chromedriver
        {

            driver.Navigate().GoToUrl(url);

            // ������� ���� �����������
            var login = driver.FindElement(By.XPath("//input[@placeholder='Username']"));
            var password = driver.FindElement(By.XPath("//input[@placeholder='Password']"));
            // ��������� ���� �����������
            login.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            // ������� ������ �����
            var loginButton = driver.FindElement(By.XPath("//input[@value='Login']"));
            // ������� �� ������ �����
            loginButton.Click();

            // ������� ������ � ����� �������
            var product = driver.FindElement(By.XPath("//img[@alt ='Sauce Labs Backpack']"));
            // ��������� �� ������ ������
            product.Click();
            // ������� ������ ���������� ������ � �������
            var cartButton = driver.FindElement(By.XPath("//button[@name = 'add-to-cart']"));
            cartButton.Click();
            // ������� ������ � ��������, � ������� ��� �������� 1 �����
            var cartLink = driver.FindElement(By.XPath("//a[@class='shopping_cart_link']"));
            // ��������� �� ���� ������
            cartLink.Click();
            string expectedResult = "Sauce Labs Backpack"; // � ������� �������� ����� ��� ������
            var actualProduct = driver.FindElement(By.XPath("//div[@class='inventory_item_name']"));
            string actualResult = actualProduct.Text;
            Assert.Equal(expectedResult, actualResult);
        }
    }
    [Fact]
    // ���� �� �������������� ������
    public void TestCheckEditProductCart()
    {
        string url = "https://www.saucedemo.com/";

        using (IWebDriver driver = new ChromeDriver()) // �������� ���������� Chromedriver
        {

            driver.Navigate().GoToUrl(url);

            // ������� ���� �����������
            var login = driver.FindElement(By.XPath("//input[@placeholder='Username']"));
            var password = driver.FindElement(By.XPath("//input[@placeholder='Password']"));
            // ��������� ���� �����������
            login.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            // ������� ������ �����
            var loginButton = driver.FindElement(By.XPath("//input[@value='Login']"));
            // ������� �� ������ �����
            loginButton.Click();

            // ������� ������ � ����� �������
            var product = driver.FindElement(By.XPath("//img[@alt ='Sauce Labs Backpack']"));
            // ��������� �� ������ ������
            product.Click();
            // ������� ������ ���������� ������ � �������
            var cartButton = driver.FindElement(By.XPath("//button[@name = 'add-to-cart']"));
            cartButton.Click();
            // ������� ������ � ��������, � ������� ��� �������� 1 �����
            var cartLink = driver.FindElement(By.XPath("//a[@class='shopping_cart_link']"));
            // ��������� �� ���� ������
            cartLink.Click();
            // �����������, ��� �� ����� �������� ��� 1 �����, ������� ������������ �� �������� �������
            var goBack = driver.FindElement(By.XPath("//button[@name = 'continue-shopping']"));
            goBack.Click();
            // ��������� �� ������ ������� ������ � ��������� ��� � �������
            var oneMoreProduct = driver.FindElement(By.XPath("//img[@alt ='Sauce Labs Fleece Jacket']"));
            oneMoreProduct.Click();
            var oneMoreCartButton = driver.FindElement(By.XPath("//button[@name = 'add-to-cart']"));
            oneMoreCartButton.Click();
            string expectedResult = "2"; // � ������� 2 ������
            var actualCart = driver.FindElement(By.XPath("//span[@class='shopping_cart_badge']"));
            string actualResult = actualCart.Text;
            Assert.Equal(expectedResult, actualResult);
        }
    }
    [Fact]
    // ���� �� ����� ������
    public void TestCheckOrderProduct()
    {
        string url = "https://www.saucedemo.com/";

        using (IWebDriver driver = new ChromeDriver()) // �������� ���������� Chromedriver
        {

            driver.Navigate().GoToUrl(url);

            // ������� ���� �����������
            var login = driver.FindElement(By.XPath("//input[@placeholder='Username']"));
            var password = driver.FindElement(By.XPath("//input[@placeholder='Password']"));
            // ��������� ���� �����������
            login.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            // ������� ������ �����
            var loginButton = driver.FindElement(By.XPath("//input[@value='Login']"));
            // ������� �� ������ �����
            loginButton.Click();

            // ������� ������ � ����� �������
            var product = driver.FindElement(By.XPath("//img[@alt ='Sauce Labs Fleece Jacket']"));
            // ��������� �� ������ ������
            product.Click();
            // ������� ������ ���������� ������ � ������� � ��������� ����� � �������
            var cartButton = driver.FindElement(By.XPath("//button[@name = 'add-to-cart']"));
            cartButton.Click();
            // ��������� � ������� �������
            var cartLink = driver.FindElement(By.XPath("//a[@class='shopping_cart_link']"));
            cartLink.Click();
            // ������� ������ ������ � �������� �� ��
            var checkOut = driver.FindElement(By.XPath("//button[@name = 'checkout']"));
            checkOut.Click();
            // ������� � ��������� ���� ��� ������
            var yourFirstName = driver.FindElement(By.XPath("//input[@placeholder='First Name']"));
            var yourLastName = driver.FindElement(By.XPath("//input[@placeholder='Last Name']"));
            var yourZipPostalCod = driver.FindElement(By.XPath("//input[@placeholder='Zip/Postal Code']"));
            yourFirstName.SendKeys("Test name");
            yourLastName.SendKeys("Test lastname");
            yourZipPostalCod.SendKeys("324687");
            // ������� ������ ��� �������� ���������� � �������� �� �������� ������������� ������
            var nextButton = driver.FindElement(By.XPath("//input[@type='submit']"));
            // ������� �� ������
            nextButton.Click();
            // ������� ������ ���������� ������ � ���������� ���� �� ���
            var finishButton = driver.FindElement(By.XPath("//button[@name='finish']"));
            finishButton.Click();
            string expectedResult = "Thank you for your order!";
            var actualHeder = driver.FindElement(By.XPath("//h2[@class='complete-header']"));
            string actualResult = actualHeder.Text;
            Assert.Equal(expectedResult, actualResult);
        }
    }
    // ���� ����� ��������� ����� �� ����������� ��������� ��� �������� ��������� �������, � �������� ������� ����� �������� �����������
    [Fact]
    // �������� ���������� ����������, ��������� ��� ��������� � ������� ����� �������
    public void TestCheckQuantityHeadersOnThisPage()
    {
        string url = "https://www.saucedemo.com/";
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(url);
            int expectedResult = 2; // 2 ���������, �������� ��� ��������� � ������� ����� ������
            // ���������, ������� ����� ����������
            var headers = driver.FindElements(By.XPath("//h1|//h2|//h3|//h4|//h5|//h6"));
            int actualResult = headers.Count;
            Assert.Equal(expectedResult, actualResult);
        }
    }
    [Fact]
    // �������� ���������� ��������� �����������, ��������� ��� ��������� � ������� ����� �������
    public void TestCheckQuantityAuthorizationElementsOnThisPage()
    {
        string url = "https://www.saucedemo.com/";
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(url);
            int expectedResult = 3; // 3 �������� �����������, �������� ��� ��������� � ������� ����� ������
            // ���������, ������� ����� ��������� �����������
            var inputs = driver.FindElements(By.TagName("input"));
            int actualResult = inputs.Count;
            Assert.Equal(expectedResult, actualResult);
        }
    }
}