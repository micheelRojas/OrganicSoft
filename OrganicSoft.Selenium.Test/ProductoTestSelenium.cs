using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace OrganicSoft.Selenium.Test
{
    public class Tests
    {
        IWebDriver driver;
        //string angularUrl = "http://localhost:4200/";
        string coreUrl = "https://localhost:5001/";

        [SetUp]
        public void Setup()
        {
            driver = GetDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void RegistroProductosSimples()
        {
            driver.Navigate().GoToUrl(coreUrl);
            NavegarEnMenu(driver, "opcion-registrar-simple");
            LlenarFormularioProductoSimple(driver);
            System.Threading.Thread.Sleep(1000);
            Assert.AreNotEqual(driver.FindElement(By.Id("swal2-title")).Text, "Error");
        }
        [Test]
        public void RegistroProductosSimplesError()
        {
            driver.Navigate().GoToUrl(coreUrl);
            NavegarEnMenu(driver, "opcion-registrar-simple");
            LlenarFormularioProductoSimple(driver);
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(driver.FindElement(By.Id("swal2-title")).Text, "Error");
        }
        [Test]
        public void RegistroProductosCombo()
        {
            driver.Navigate().GoToUrl(coreUrl);
            NavegarEnMenu(driver, "opcion-registrar-combo");
            LlenarFormularioProductoCombo(driver);
            System.Threading.Thread.Sleep(1000);
            Assert.AreNotEqual(driver.FindElement(By.Id("swal2-title")).Text, "Error");
        }
        [Test]
        public void RegistroProductosComboError()
        {
            driver.Navigate().GoToUrl(coreUrl);
            NavegarEnMenu(driver, "opcion-registrar-combo");
            LlenarFormularioProductoCombo(driver);
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(driver.FindElement(By.Id("swal2-title")).Text, "Error");
        }
        [Test]
        public void RegistroEntradaProducto()
        {
            driver.Navigate().GoToUrl(coreUrl);
            NavegarEnMenu(driver, "consultar-productos");
            System.Threading.Thread.Sleep(1000);
            LlenardeRegistroEntrada(driver,"1");
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(driver.FindElement(By.Id("swal2-title")).Text, "Exitoso!");
        }
       [Test]
        public void RegistroEntradaProductoError()
        {
            driver.Navigate().GoToUrl(coreUrl);
            NavegarEnMenu(driver, "consultar-productos");
            System.Threading.Thread.Sleep(1000);
            LlenardeRegistroEntrada(driver,"0");
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(driver.FindElement(By.Id("swal2-title")).Text, "Error");
        }
        [Test]
        public void RegistrarCarritoCompra()
        {
            driver.Navigate().GoToUrl(coreUrl);
            NavegarEnMenu(driver, "crear-carrito");
            System.Threading.Thread.Sleep(1000);
            LlenardeRegistroCrearCarrito(driver);
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(driver.FindElement(By.Id("swal2-title")).Text, "¡Exitoso!");
        }
        [Test]
        public void RegistrarCarritoCompraError()
        {
            driver.Navigate().GoToUrl(coreUrl);
            NavegarEnMenu(driver, "crear-carrito");
            System.Threading.Thread.Sleep(1000);
            LlenardeRegistroCrearCarrito(driver);
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(driver.FindElement(By.Id("swal2-title")).Text, "Error");
        }

        [Test]
        public void LlenarCarritodeCompraCorrecto()
        {
            driver.Navigate().GoToUrl(coreUrl);
            NavegarEnMenu(driver, "opcion-carrito");
            System.Threading.Thread.Sleep(2000);
            LLenarCarrito(driver, "1");
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(driver.FindElement(By.Id("swal2-title")).Text, "¡Exitoso!");
        }
        [Test]
        public void LlenarCarritodeCompraError()
        {
            driver.Navigate().GoToUrl(coreUrl);
            NavegarEnMenu(driver, "opcion-carrito");
            System.Threading.Thread.Sleep(2000);
            LLenarCarrito(driver, "0");
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(driver.FindElement(By.Id("swal2-title")).Text, "Error");
        }

       [Test]
        public void FinalizarCompra()
        {
            System.Threading.Thread.Sleep(3000);
            driver.Navigate().GoToUrl(coreUrl);
            NavegarEnMenu(driver, "opcion-carrito");
            System.Threading.Thread.Sleep(3000);
            FinalizarCompraCarrito(driver);
            System.Threading.Thread.Sleep(3000);
            Assert.AreEqual(driver.FindElement(By.Id("swal2-title")).Text, "¡Exitoso!");
        }
        [Test]
        public void FinalizarCompraError()
        {
            driver.Navigate().GoToUrl(coreUrl);
            NavegarEnMenu(driver, "opcion-carrito");
            System.Threading.Thread.Sleep(3000);
            FinalizarCompraCarrito(driver);
            System.Threading.Thread.Sleep(3000);
            Assert.AreEqual(driver.FindElement(By.Id("swal2-title")).Text, "Error");
        }
        /// Configura el driver para Chrome
        private IWebDriver GetDriver()
        {
            var user_agent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36";
            ChromeOptions options = new ChromeOptions();
            //Descomenta esta linea para usar el mode HeadLess de Chrome
            options.AddArgument("--headless");
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
        private void NavegarEnMenu(IWebDriver driver,string ir)
        {
            driver.FindElement(By.Id(ir)).Click();
            System.Threading.Thread.Sleep(100);
        }
        private static void LlenarFormularioProductoSimple(IWebDriver driver)
        {
            //mirar como mandar un codigo difertente cada ves que eejectuta
           
            driver.FindElement(By.Id("codigoProducto")).SendKeys("123");
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
        private static void LlenarFormularioProductoCombo(IWebDriver driver)
        {
            //mirar como mandar un codigo difertente cada ves que eejectuta
            driver.FindElement(By.Id("selecionar-componete")).Click();
            System.Threading.Thread.Sleep(100);
            driver.FindElement(By.Id("boton-confirmar")).Click();
            System.Threading.Thread.Sleep(100);
            driver.FindElement(By.Id("codigoProducto")).SendKeys("987978");
            driver.FindElement(By.Id("nombre")).SendKeys("Jabón Banana");
            driver.FindElement(By.Id("descripcion")).SendKeys("Hidrata y Humecta");
            driver.FindElement(By.Id("precio")).SendKeys("10000");
            driver.FindElement(By.Id("categoria")).SendKeys("Jabón");
            driver.FindElement(By.Id("presentacion")).SendKeys("80 gr Grande");
            driver.FindElement(By.Id("minimoStock")).SendKeys("1");
            driver.FindElement(By.Id("boton-guardar-combo")).Click();
            System.Threading.Thread.Sleep(100);
        }
        private static void LlenardeRegistroEntrada(IWebDriver driver, string cantidad)
        {
            //mirar como mandar un codigo difertente cada ves que eejectuta
            driver.FindElement(By.Id("registrar-entrada")).Click();
            System.Threading.Thread.Sleep(100);
            driver.FindElement(By.Id("cantidad-registar")).SendKeys(cantidad);
            System.Threading.Thread.Sleep(100);
            driver.FindElement(By.Id("boton-confirmar")).Click();
            System.Threading.Thread.Sleep(100);
        }

        private static void LlenardeRegistroCrearCarrito(IWebDriver driver)
        {
            //mirar como mandar un codigo difertente cada ves que eejectuta
            System.Threading.Thread.Sleep(100);
            driver.FindElement(By.Id("codigo")).SendKeys("123");
            driver.FindElement(By.Id("cedulaCliente")).SendKeys("125538");
            System.Threading.Thread.Sleep(100);
            driver.FindElement(By.Id("boton-guardar-carrito")).Click();
            System.Threading.Thread.Sleep(100);
        }
        private static void LLenarCarrito(IWebDriver driver, string cantidad)
        {
            //mirar como mandar un codigo difertente cada ves que eejectuta
            System.Threading.Thread.Sleep(100);
            driver.FindElement(By.Id("llenar-carrito")).Click();
            System.Threading.Thread.Sleep(500);
            driver.FindElement(By.Id("lo-quiero")).Click();
            System.Threading.Thread.Sleep(100);
            driver.FindElement(By.Id("cantidad-registar")).SendKeys(cantidad);
            System.Threading.Thread.Sleep(100);
            driver.FindElement(By.Id("boton-confirmar")).Click();
            System.Threading.Thread.Sleep(100);

           
        }
        private static void FinalizarCompraCarrito(IWebDriver driver)
        {
            //mirar como mandar un codigo difertente cada ves que eejectuta
            System.Threading.Thread.Sleep(100);
            driver.FindElement(By.Id("ver")).Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Id("finalizar")).Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Id("codigo-registar")).SendKeys("129");
            System.Threading.Thread.Sleep(100);
            driver.FindElement(By.Id("boton-confirmar")).Click();
            System.Threading.Thread.Sleep(100);


        }
        


    }
}