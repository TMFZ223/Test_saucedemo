using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

public class TestSaucedemo
{
    [Fact]
    // Тест на авторизацию
    public void TestCheckAuthorization()
    {
        string url = "https://www.saucedemo.com/";

        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(url);

            // Находим поля авторизации
            var login = driver.FindElement(By.XPath("//input[@placeholder='Username']"));
            var password = driver.FindElement(By.XPath("//input[@placeholder='Password']"));
            // Заполняем поля авторизации
            login.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            // находим кнопку входа
            var loginButton = driver.FindElement(By.XPath("//input[@value='Login']"));
            // Кликаем по кнопке входа
            loginButton.Click();
            string expectedResult = "https://www.saucedemo.com/inventory.html"; // Ожидаемый адрес перехода после авторизации
            var actualResult = driver.Url; // Фактический результат
            Assert.Equal(expectedResult, actualResult);
        }
    }
    [Fact]
    // Тест на добавление товаров в корзину
    public void TestCheckAddProductCart()
    {
        string url = "https://www.saucedemo.com/";

        using (IWebDriver driver = new ChromeDriver()) // Создание экземпляра Chromedriver
        {

            driver.Navigate().GoToUrl(url);

            // Находим поля авторизации
            var login = driver.FindElement(By.XPath("//input[@placeholder='Username']"));
            var password = driver.FindElement(By.XPath("//input[@placeholder='Password']"));
            // Заполняем поля авторизации
            login.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            // находим кнопку входа
            var loginButton = driver.FindElement(By.XPath("//input[@value='Login']"));
            // Кликаем по кнопке входа
            loginButton.Click();

            // Находим ссылку с любым товаром
            var product = driver.FindElement(By.XPath("//img[@alt ='Sauce Labs Backpack']"));
            // Переходим по ссылке товара
            product.Click();
            // Находим кнопку добавления товара в корзину
            var cartButton = driver.FindElement(By.XPath("//button[@name = 'add-to-cart']"));
            cartButton.Click();
            // Находим ссылку с корзиной, в которую был добавлен 1 товар
            var cartLink = driver.FindElement(By.XPath("//a[@class='shopping_cart_link']"));
            // Переходим по этой ссылке
            cartLink.Click();
            string expectedResult = "Sauce Labs Backpack"; // В корзине доступен товар для заказа
            var actualProduct = driver.FindElement(By.XPath("//div[@class='inventory_item_name']"));
            string actualResult = actualProduct.Text;
            Assert.Equal(expectedResult, actualResult);
        }
    }
    [Fact]
    // Тест на редактирование заказа
    public void TestCheckEditProductCart()
    {
        string url = "https://www.saucedemo.com/";

        using (IWebDriver driver = new ChromeDriver()) // Создание экземпляра Chromedriver
        {

            driver.Navigate().GoToUrl(url);

            // Находим поля авторизации
            var login = driver.FindElement(By.XPath("//input[@placeholder='Username']"));
            var password = driver.FindElement(By.XPath("//input[@placeholder='Password']"));
            // Заполняем поля авторизации
            login.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            // находим кнопку входа
            var loginButton = driver.FindElement(By.XPath("//input[@value='Login']"));
            // Кликаем по кнопке входа
            loginButton.Click();

            // Находим ссылку с любым товаром
            var product = driver.FindElement(By.XPath("//img[@alt ='Sauce Labs Backpack']"));
            // Переходим по ссылке товара
            product.Click();
            // Находим кнопку добавления товара в корзину
            var cartButton = driver.FindElement(By.XPath("//button[@name = 'add-to-cart']"));
            cartButton.Click();
            // Находим ссылку с корзиной, в которую был добавлен 1 товар
            var cartLink = driver.FindElement(By.XPath("//a[@class='shopping_cart_link']"));
            // Переходим по этой ссылке
            cartLink.Click();
            // Предположим, что мы хотим добавить ещё 1 товар, поэтому возвращаемся на страницу товаров
            var goBack = driver.FindElement(By.XPath("//button[@name = 'continue-shopping']"));
            goBack.Click();
            // Переходим по ссылке другого товара и добавляем его в корзину
            var oneMoreProduct = driver.FindElement(By.XPath("//img[@alt ='Sauce Labs Fleece Jacket']"));
            oneMoreProduct.Click();
            var oneMoreCartButton = driver.FindElement(By.XPath("//button[@name = 'add-to-cart']"));
            oneMoreCartButton.Click();
            string expectedResult = "2"; // В корзине 2 товара
            var actualCart = driver.FindElement(By.XPath("//span[@class='shopping_cart_badge']"));
            string actualResult = actualCart.Text;
            Assert.Equal(expectedResult, actualResult);
        }
    }
    [Fact]
    // Тест на заказ товара
    public void TestCheckOrderProduct()
    {
        string url = "https://www.saucedemo.com/";

        using (IWebDriver driver = new ChromeDriver()) // Создание экземпляра Chromedriver
        {

            driver.Navigate().GoToUrl(url);

            // Находим поля авторизации
            var login = driver.FindElement(By.XPath("//input[@placeholder='Username']"));
            var password = driver.FindElement(By.XPath("//input[@placeholder='Password']"));
            // Заполняем поля авторизации
            login.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            // находим кнопку входа
            var loginButton = driver.FindElement(By.XPath("//input[@value='Login']"));
            // Кликаем по кнопке входа
            loginButton.Click();

            // Находим ссылку с любым товаром
            var product = driver.FindElement(By.XPath("//img[@alt ='Sauce Labs Fleece Jacket']"));
            // Переходим по ссылке товара
            product.Click();
            // Находим кнопку добавления товара в корзину и добавляем товар в корзину
            var cartButton = driver.FindElement(By.XPath("//button[@name = 'add-to-cart']"));
            cartButton.Click();
            // Переходим в корзину товаров
            var cartLink = driver.FindElement(By.XPath("//a[@class='shopping_cart_link']"));
            cartLink.Click();
            // Находим кнопку заказа и нажимаем на неё
            var checkOut = driver.FindElement(By.XPath("//button[@name = 'checkout']"));
            checkOut.Click();
            // Находим и заполняем поля для заказа
            var yourFirstName = driver.FindElement(By.XPath("//input[@placeholder='First Name']"));
            var yourLastName = driver.FindElement(By.XPath("//input[@placeholder='Last Name']"));
            var yourZipPostalCod = driver.FindElement(By.XPath("//input[@placeholder='Zip/Postal Code']"));
            yourFirstName.SendKeys("Test name");
            yourLastName.SendKeys("Test lastname");
            yourZipPostalCod.SendKeys("324687");
            // Находим кнопку для отправки информации и перехода на страницу подтверждения заказа
            var nextButton = driver.FindElement(By.XPath("//input[@type='submit']"));
            // Кликаем по кнопке
            nextButton.Click();
            // Находим кнопку завершения заказа и производим клик по ней
            var finishButton = driver.FindElement(By.XPath("//button[@name='finish']"));
            finishButton.Click();
            string expectedResult = "Thank you for your order!";
            var actualHeder = driver.FindElement(By.XPath("//h2[@class='complete-header']"));
            string actualResult = actualHeder.Text;
            Assert.Equal(expectedResult, actualResult);
        }
    }
    // Ниже будут приведены тесты на доступность элементов для программ экранного доступа, в качестве образца взята страница авторизации
    [Fact]
    // Проверка количества заголовков, доступных для просмотра с помощью скрин ридеров
    public void TestCheckQuantityHeadersOnThisPage()
    {
        string url = "https://www.saucedemo.com/";
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(url);
            int expectedResult = 2; // 2 заголовка, доступно для просмотра с помощью скрин ридера
            // Проверяем, сколько всего заголовков
            var headers = driver.FindElements(By.XPath("//h1|//h2|//h3|//h4|//h5|//h6"));
            int actualResult = headers.Count;
            Assert.Equal(expectedResult, actualResult);
        }
    }
    [Fact]
    // Проверка количества элементов авторизации, доступных для просмотра с помощью скрин ридеров
    public void TestCheckQuantityAuthorizationElementsOnThisPage()
    {
        string url = "https://www.saucedemo.com/";
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(url);
            int expectedResult = 3; // 3 элемента авторизации, доступно для просмотра с помощью скрин ридера
            // Проверяем, сколько всего элементов авторизации
            var inputs = driver.FindElements(By.TagName("input"));
            int actualResult = inputs.Count;
            Assert.Equal(expectedResult, actualResult);
        }
    }
}