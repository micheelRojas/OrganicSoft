using NUnit.Framework;
using OrganicSoft.Dominio;
using System;
using System.Collections.Generic;

namespace OrganicSoft.Test.Facturas
{
    class CarritoCompraTest
    {
        /*
      * Como usuario 
      * quiero agregar productos a mi carrito de compras 
      * para poder generar pedidos
      */
        [Test]
        public void PuedoAgregarProductoAlCarritoCompra()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente cuenta con un carrito de compras
            var jabonSandia = new Producto(codigo: 1, nombre: "Jabón de Sandía",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new Producto(codigo: 2, nombre: "Exfoliante Mujer",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);

            CarritoCompra carrito = new CarritoCompra(cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);

            #endregion
            #region CUANDO el cliente desea agregar productos al carrito de compra
            var respuesta = carrito.AgregarAlCarrito(productoVenta);
            #endregion
            #region ENTONCES  el sistema agregará el producto en el carrito y mostrara el mensaje "Se ha agregado 2 unidades del producto Jabón de Sandía"
            Assert.AreEqual("Se ha agregado 2 unidades del producto Jabón de Sandía", respuesta);
            #endregion

        }

        [Test]
        public void NoExisteProductoParaAgregarAlCarritoCompra()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente cuenta con un carrito de compras
            var jabonSandia = new Producto(codigo: 1, nombre: "Jabón de Sandía",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new Producto(codigo: 2, nombre: "Exfoliante Mujer",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);

            CarritoCompra carrito = new CarritoCompra(cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 3, cantidadVenta: 2);

            #endregion
            #region CUANDO el cliente desea agregar productos al carrito de compra y el producto no existe
            var respuesta = carrito.AgregarAlCarrito(productoVenta);
            #endregion
            #region ENTONCES  el sistema mostrará el mensaje "No se encontró el producto"
            Assert.AreEqual("No se encontró el producto", respuesta);
            #endregion

        }


        [Test]
        public void NoPuedoAgregarCantidadProductoMenosUno()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente cuenta con un carrito de compras
            var jabonSandia = new Producto(codigo: 1, nombre: "Jabón de Sandía",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new Producto(codigo: 2, nombre: "Exfoliante Mujer",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);

            CarritoCompra carrito = new CarritoCompra(cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 3, cantidadVenta: -1);

            #endregion
            #region CUANDO el cliente desea agregar productos al carrito de compra y la cantidad de productos es -1
            var respuesta = carrito.AgregarAlCarrito(productoVenta);
            #endregion
            #region ENTONCES  el sistema mostrará el mensaje "La cantidad del producto debe ser mayor a cero"
            Assert.AreEqual("La cantidad del producto debe ser mayor a cero", respuesta);
            #endregion
        }

        [Test]
        public void NoPuedoEliminarProductoDelCarrito()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente cuenta con un carrito de compras que tiene un producto
            var jabonSandia = new Producto(codigo: 1, nombre: "Jabón de Sandía",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new Producto(codigo: 2, nombre: "Exfoliante Mujer",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);

            CarritoCompra carrito = new CarritoCompra(cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);

            #endregion
            #region CUANDO el cliente desea eliminar el producto del carrito y el producto no existe
            var respuesta = carrito.EliminarDelCarrito(codigoProductoVenta: 3);
            #endregion
            #region ENTONCES  el sistema mostrará el mensaje "No se pudo eliminar el producto"
            Assert.AreEqual("No se pudo eliminar el producto", respuesta);
            #endregion
        }

        [Test]
        public void PuedoEliminarProductoDelCarrito()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente cuenta con un carrito de compras que tiene un producto
            var jabonSandia = new Producto(codigo: 1, nombre: "Jabón de Sandía",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new Producto(codigo: 2, nombre: "Exfoliante Mujer",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);

            CarritoCompra carrito = new CarritoCompra(cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);

            #endregion
            #region CUANDO el cliente desea eliminar el producto del carrito
            var respuesta = carrito.EliminarDelCarrito(codigoProductoVenta: 1);
            #endregion
            #region ENTONCES  el sistema eliminará el producto del carrito y mostrará el mensaje "Se eliminó el productó Jabón de Sandía correctamente"
            Assert.AreEqual("Se eliminó el productó correctamente", respuesta);
            #endregion
        }

        [Test]
        public void PuedoDisminuirCantidadProductoDelCarrito()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente cuenta con un carrito de compras que tiene un producto
            var jabonSandia = new Producto(codigo: 1, nombre: "Jabón de Sandía",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new Producto(codigo: 2, nombre: "Exfoliante Mujer",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);

            CarritoCompra carrito = new CarritoCompra(cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);

            #endregion
            #region CUANDO el cliente desea disminuir la cantidad del producto 
            int cantidadDisminuir = 1;
            var respuesta = carrito.DisminuirCantidadProducto(codigoProductoVenta: 1, cantidad: cantidadDisminuir);
            #endregion
            #region ENTONCES  el sistema disminuirá la cantidad del producto y mostrará el mensaje "La nueva cantidad del producto Jabón de Sandía en el carrito es 1"
            Assert.AreEqual("La nueva cantidad del producto Jabón de Sandía en el carrito es 1", respuesta);
            #endregion
        }

        [Test]
        public void NoPuedoDisminuirCantidadProductoDelCarrito()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente cuenta con un carrito de compras que tiene un producto
            var jabonSandia = new Producto(codigo: 1, nombre: "Jabón de Sandía",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new Producto(codigo: 2, nombre: "Exfoliante Mujer",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);

            CarritoCompra carrito = new CarritoCompra(cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);

            #endregion
            #region CUANDO el cliente desea disminuir la cantidad del producto y dicha cantidad es mayor a la que hay en el carrito actualmente 
            int cantidadDisminuir = 3;
            var respuesta = carrito.DisminuirCantidadProducto(codigoProductoVenta: 1, cantidad: cantidadDisminuir);
            #endregion
            #region ENTONCES  el sistema no disminuirá la cantidad del producto y mostrará el mensaje "La cantidad a disminuir debe ser menor a la cantidad existente en el carrito"
            Assert.AreEqual("La cantidad a disminuir debe ser menor a la cantidad existente en el carrito", respuesta);
            #endregion
        }

        [Test]
        public void NoPuedoDisminuirCantidadProductoDelCarritoMenosUno()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente cuenta con un carrito de compras que tiene un producto
            var jabonSandia = new Producto(codigo: 1, nombre: "Jabón de Sandía",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new Producto(codigo: 2, nombre: "Exfoliante Mujer",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);

            CarritoCompra carrito = new CarritoCompra(cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);

            #endregion
            #region CUANDO el cliente desea disminuir la cantidad del producto y dicha cantidad es -1
            int cantidadDisminuir = -1;
            var respuesta = carrito.DisminuirCantidadProducto(codigoProductoVenta: 1, cantidad: cantidadDisminuir);
            #endregion
            #region ENTONCES  el sistema no disminuirá la cantidad del producto y mostrará el mensaje "La cantidad a disminuir debe ser mayor a cero"
            Assert.AreEqual("La cantidad a disminuir debe ser mayor a cero", respuesta);
            #endregion
        }

        [Test]
        public void NoExisteProductoDelCarritoADisminuir()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente cuenta con un carrito de compras que tiene un producto
            var jabonSandia = new Producto(codigo: 1, nombre: "Jabón de Sandía",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new Producto(codigo: 2, nombre: "Exfoliante Mujer",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);

            CarritoCompra carrito = new CarritoCompra(cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);

            #endregion
            #region CUANDO el cliente desea disminuir la cantidad del producto y el producto no existe
            int cantidadDisminuir = 1;
            var respuesta = carrito.DisminuirCantidadProducto(codigoProductoVenta: 3, cantidad: cantidadDisminuir);
            #endregion
            #region ENTONCES  el sistema no disminuirá la cantidad del producto y mostrará el mensaje "No se encontró el producto"
            Assert.AreEqual("No se encontró el producto", respuesta);
            #endregion
        }
        [Test]
        public void PuedoAumentarCantidadProductoDelCarrito()
        {

            #region Dado que laly Organis tiene multiples productos, como jabon de sandia, exfoliante y el cliente cuenta con un carrito de compras que tiene un producto
            var jabonSandia = new Producto(codigo: 1, nombre: "Jabón de Sandía",
            decripcion: " Ea hidrante facial y corporal 🍉La sandía es rica en antioxidantes, ayuda a" +
            " retrasar el envejecimiento de la piel debido a su protección contra los radicales libres." +
            " Gracias a estas propiedades, previene los primeros síntomas de la edad, como manchas, " +
            "arrugas y unas líneas de expresión marcadas.", costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            var exfoliante = new Producto(codigo: 2, nombre: "Exfoliante Mujer",
            decripcion: "Un exfoliante es un producto hecho principalmente a base de ingredientes naturales que sirve para remover las impurezas y células muertas de los labios",
            costo: 6000.00, precio: 10000.00, categoria: "Jabon", presentacion: "pequeño, 80 gr", minimoStock: 3);
            jabonSandia.EntradaProductos(cantidad: 10);
            exfoliante.EntradaProductos(cantidad: 10);

            CarritoCompra carrito = new CarritoCompra(cedulaCliente: "1002353645");
            ProductoVenta productoVenta = new ProductoVenta(codigoProducto: 1, cantidadVenta: 2);
            carrito.AgregarAlCarrito(productoVenta);

            #endregion
            #region CUANDO el cliente desea aumentar  la cantidad del producto 
            int cantidadAumentar = 1;
            var respuesta = carrito.AumentarCantidadProducto(codigoProductoVenta: 1, cantidad: cantidadAumentar);
            #endregion
            #region ENTONCES  el sistema aumentara  la cantidad del producto y mostrará el mensaje "La nueva cantidad del producto Jabón de Sandía en el carrito es 3"
            Assert.AreEqual("La nueva cantidad del producto Jabón de Sandía en el carrito es 3", respuesta);
            #endregion
        }
    }
}


internal class ProductoVenta
{
    public int CodigoProducto { get; private set; }
    public int CantidadVenta { get; private set; }

    public ProductoVenta(int codigoProducto, int cantidadVenta)
    {
        CodigoProducto = codigoProducto;
        CantidadVenta = cantidadVenta;
    }

    public int AumentarCantidadProductoVenta(int cantidad)
    {
        return CantidadVenta += cantidad;
    }

    public int DisminuirCantidadProductoVenta(int cantidad)
    {
        return CantidadVenta -= cantidad;
    }
}

internal class CarritoCompra
{
    public string CedulaCliente { get; private set; }
    protected List<ProductoVenta> _productoVentas;

    public CarritoCompra(string cedulaCliente)
    {
        CedulaCliente = cedulaCliente;
        _productoVentas = new List<ProductoVenta>();
    }
    public IReadOnlyCollection<ProductoVenta> ProductosVenta => _productoVentas.AsReadOnly();

    internal string AgregarAlCarrito(ProductoVenta productoVenta)
    {
        String respuesta = "No se encontró el producto";
        if (productoVenta.CantidadVenta <= 0)
        {
            respuesta = "La cantidad del producto debe ser mayor a cero";
            return respuesta;
        }
        foreach (var producto in Producto.Productos)
        {
            if (productoVenta.CodigoProducto.Equals(producto.CodigoProducto))
            {
                _productoVentas.Add(productoVenta);
                respuesta = $"Se ha agregado {productoVenta.CantidadVenta} unidades del producto {producto.Nombre}";
            }
        }
        return respuesta;
    }

    internal string EliminarDelCarrito(int codigoProductoVenta)
    {

        String respuesta = "No se pudo eliminar el producto";
        foreach (var producto in ProductosVenta)
        {
            if (producto.CodigoProducto.Equals(codigoProductoVenta))
            {
                _productoVentas.Remove(producto);
                respuesta = "Se eliminó el productó correctamente";
                return respuesta;
            }
        }
        return respuesta;
    }

    internal string DisminuirCantidadProducto(int codigoProductoVenta, int cantidad)
    {
        String respuesta = "No se encontró el producto";
        if (cantidad <= 0)
        {
            respuesta = "La cantidad a disminuir debe ser mayor a cero";
            return respuesta;
        }
        foreach (var productoVenta in ProductosVenta)
        {
            if (codigoProductoVenta.Equals(productoVenta.CodigoProducto))
            {
                if (productoVenta.CantidadVenta > cantidad)
                {
                    productoVenta.DisminuirCantidadProductoVenta(cantidad);
                    foreach (var producto in Producto.Productos)
                    {
                        if (codigoProductoVenta.Equals(producto.CodigoProducto))
                        {
                            return respuesta = $"La nueva cantidad del producto {producto.Nombre} en el carrito es {productoVenta.CantidadVenta}";

                        }
                    }
                }
                else if (productoVenta.CantidadVenta <= cantidad)
                {
                    respuesta = "La cantidad a disminuir debe ser menor a la cantidad existente en el carrito";
                }


            }
        }

        return respuesta;
    }

    internal string AumentarCantidadProducto(int codigoProductoVenta, int cantidad)
    {
        String respuesta = "No se encontró el producto";
        if (cantidad <= 0)
        {
            respuesta = "La cantidad a aumentar debe ser mayor a cero";
            return respuesta;
        }
        foreach (var productoVenta in ProductosVenta)
        {
            if (codigoProductoVenta.Equals(productoVenta.CodigoProducto))
            {
                productoVenta.AumentarCantidadProductoVenta(cantidad);
                foreach (var producto in Producto.Productos)
                {
                    if (codigoProductoVenta.Equals(producto.CodigoProducto))
                    {
                        return respuesta = $"La nueva cantidad del producto {producto.Nombre} en el carrito es {productoVenta.CantidadVenta}";

                    }
                }
            }
        }
        return respuesta;
    }
}

