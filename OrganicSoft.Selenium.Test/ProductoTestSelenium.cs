using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace OrganicSoft.Selenium.Test
{
    public class Tests
    {
        IWebDriver driver;
        string angularUrl = "http://localhost:4200/";
        string coreUrl = "https://localhost:5001/";

        [SetUp]
        public void Setup()
        {
            driver = GetDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void ProductosSimples()
        {
            driver.Navigate().GoToUrl(coreUrl);
            NavegarHaciaRegistrarProductos(driver);
            LlenarFormularioProductoSimple(driver);
            System.Threading.Thread.Sleep(1000);
            Assert.AreNotEqual(driver.FindElement(By.Id("swal2-title")).Text, "Error");
        }
        /// Configura el driver para Chrome
        private IWebDriver GetDriver()
        {
            var user_agent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36";
            ChromeOptions options = new ChromeOptions();
            //Descomenta esta linea para usar el mode HeadLess de Chrome
            //options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument($"user_agent={user_agent}");
            options.AddArgument("--ignore-certificate-errors");
            IWebDriver driver = new ChromeDriver(Directory.GetCurrentDirectory(), options);
            return driver;
        }

        //[TearDown]
        //public void despuesTest() {
        //    if (driver!=null)
        //    {
        //        driver.Quit();
        //    }
        //}
        private void NavegarHaciaRegistrarProductos(IWebDriver driver)
        {
            driver.FindElement(By.Id("opcion-registrar-simple")).Click();
            System.Threading.Thread.Sleep(100);
        }
        private static void LlenarFormularioProductoSimple(IWebDriver driver)
        {
            //mirar como mandar un codigo difertente cada ves que eejectuta
           
            driver.FindElement(By.Id("codigoProducto")).SendKeys("123457");
            driver.FindElement(By.Id("nombre")).SendKeys("Jabón Banana");
            driver.FindElement(By.Id("descripcion")).SendKeys("Hidrata y Humecta");
            driver.FindElement(By.Id("precio")).SendKeys("10000");
            driver.FindElement(By.Id("categoria")).SendKeys("Jabón");
            driver.FindElement(By.Id("presentacion")).SendKeys("80 gr Grande");
            driver.FindElement(By.Id("minimoStock")).SendKeys("1");
            driver.FindElement(By.Id("costo")).SendKeys("5000");
            driver.FindElement(By.Id("boton-guardar-simple")).Click();
            System.Threading.Thread.Sleep(100);
        }
       
    }
}