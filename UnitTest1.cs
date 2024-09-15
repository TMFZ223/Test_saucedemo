using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit;

public class TestSaucedemo
{
    private IWebDriver driver;
    // Создание конструктора класса
    public TestSaucedemo()
    {
        driver = new ChromeDriver();
        setup();
    }
    // Функция, которая будет выполняться перед каждым тестом
    public void setup()
    {
        string url = "https://www.saucedemo.com/";
        driver.Navigate().GoToUrl(url);
    }
    // Метод авторизации
    private void authorize(IWebDriver driver)
    {
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
    }
    // Метод для добавления товара в корзину
    private void addProductToCart(IWebDriver driver)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        // Находим ссылку с любым товаром
        var product = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@alt ='Sauce Labs Backpack']")));
        // Переходим по ссылке товара
        product.Click();
        // Находим кнопку добавления товара в корзину
        var cartButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@name = 'add-to-cart']")));
        cartButton.Click();
        // Находим ссылку с корзиной, в которую был добавлен 1 товар
        var cartLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@class='shopping_cart_link']")));
        // Переходим по этой ссылке
        cartLink.Click();
    }
    private void continueShopping(IWebDriver driver)
    {
        // Предположим, что мы хотим добавить ещё 1 товар, поэтому возвращаемся на страницу товаров
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        var goBack = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@name = 'continue-shopping']")));
        goBack.Click();
        // Переходим по ссылке другого товара и добавляем его в корзину
        var oneMoreProduct = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@alt ='Sauce Labs Fleece Jacket']")));
        oneMoreProduct.Click();
        var oneMoreCartButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@name = 'add-to-cart']")));
        oneMoreCartButton.Click();
    }
    // Метод заказа товара
    private void doOrder(IWebDriver driver)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        // Находим ссылку с любым товаром
        var product = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@alt ='Sauce Labs Fleece Jacket']")));
        // Переходим по ссылке товара
        product.Click();
        // Находим кнопку добавления товара в корзину и добавляем товар в корзину
        var cartButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@name = 'add-to-cart']")));
        cartButton.Click();
        // Переходим в корзину товаров
        var cartLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@class='shopping_cart_link']")));
        cartLink.Click();
        // Находим кнопку заказа и нажимаем на неё
        var checkOut = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@name = 'checkout']")));
        checkOut.Click();
        // Находим и заполняем поля для заказа
        var yourFirstName = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='First Name']")));
        var yourLastName = driver.FindElement(By.XPath("//input[@placeholder='Last Name']"));
        var yourZipPostalCod = driver.FindElement(By.XPath("//input[@placeholder='Zip/Postal Code']"));
        yourFirstName.SendKeys("Test name");
        yourLastName.SendKeys("Test lastname");
        yourZipPostalCod.SendKeys("324687");
        // Находим кнопку для отправки информации и перехода на страницу подтверждения заказа
        var nextButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@type='submit']")));
        // Кликаем по кнопке
        nextButton.Click();
        // Находим кнопку завершения заказа и производим клик по ней
        var finishButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@name='finish']")));
        finishButton.Click();
    }
    [Fact]
    // Тест на авторизацию
    public void TestCheckAuthorization()
    {
        // Выполняем авторизацию
        authorize(driver);
        string expectedResult = "https://www.saucedemo.com/inventory.html"; // Ожидаемый адрес перехода после авторизации
        var actualResult = driver.Url; // Фактический результат
        Assert.Equal(expectedResult, actualResult);
        driver.Quit();
    }
    [Fact]
    // Тест на добавление товаров в корзину
    public void TestCheckAddProductCart()
    {
        // Выполняем авторизацию
        authorize(driver);
        // Осуществляем добавление товара в корзину
        addProductToCart(driver);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        var actualProduct = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='inventory_item_name']")));
        string expectedResult = "Sauce Labs Backpack"; // В корзине доступен товар для заказа
        string actualResult = actualProduct.Text;
        Assert.Equal(expectedResult, actualResult);
        driver.Quit();
    }
    [Fact]
    // Тест на редактирование заказа
    public void TestCheckEditProductCart()
    {
        // Выполняем авторизацию
        authorize(driver);
        // Осуществляем добавление товара в корзину
        addProductToCart(driver);
        // Продолжаем покупку
        continueShopping(driver);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        var actualCart = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='shopping_cart_badge']")));
        string expectedResult = "2"; // В корзине 2 товара
        string actualResult = actualCart.Text;
        Assert.Equal(expectedResult, actualResult);
        driver.Quit();
    }
    [Fact]
    // Тест на заказ товара
    public void TestCheckOrderProduct()
    {
        // Выполняем авторизацию
        authorize(driver);
        // Выполняем заказ товара
        doOrder(driver);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        var actualHeder = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[@class='complete-header']")));
        string expectedResult = "Thank you for your order!";
        string actualResult = actualHeder.Text;
        Assert.Equal(expectedResult, actualResult);
        driver.Quit();
    }
    // Ниже будут приведены тесты на доступность элементов для программ экранного доступа, в качестве образца взята страница авторизации
    [Fact]
    // Проверка количества заголовков, доступных для просмотра с помощью скрин ридеров
    public void TestCheckQuantityHeadersOnThisPage()
    {
        // Проверяем, сколько всего заголовков
        var headers = driver.FindElements(By.XPath("//h1|//h2|//h3|//h4|//h5|//h6"));
        int expectedResult = 2; // 2 заголовка, доступно для просмотра с помощью скрин ридера
        int actualResult = headers.Count;
        Assert.Equal(expectedResult, actualResult);
        driver.Quit();
    }
    [Fact]
    // Проверка количества элементов авторизации, доступных для просмотра с помощью скрин ридеров
    public void TestCheckQuantityAuthorizationElementsOnThisPage()
    {
        // Проверяем, сколько всего элементов авторизации
        var inputs = driver.FindElements(By.TagName("input"));
        int expectedResult = 3; // 3 элемента авторизации, доступно для просмотра с помощью скрин ридера
        int actualResult = inputs.Count;
        Assert.Equal(expectedResult, actualResult);
        driver.Quit();
    }
}
