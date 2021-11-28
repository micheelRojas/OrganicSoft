import { Component, OnInit } from '@angular/core';
import { IProducto } from '../producto/producto.component';

@Component({
  selector: 'app-carrito-compra',
  templateUrl: './carrito-compra.component.html',
  styleUrls: ['./carrito-compra.component.css']
})
export class CarritoCompraComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }


}
export interface ICarritoCompra {
  id: number,
  codigo: number,
  cedulaCliente : string
}
export interface IAggCarritoCompra {
  id: number,
  productoVenta: IProductoVenta
}
export interface IProductoVenta {
  codigoProducto: number,
  cantidadVenta:number
}

