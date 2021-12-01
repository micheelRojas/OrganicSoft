import { Component, OnInit } from '@angular/core';
import { ICarritoCompra } from '../carrito-compra/carrito-compra.component';

@Component({
  selector: 'app-pedido',
  templateUrl: './pedido.component.html',
  styleUrls: ['./pedido.component.css']
})
export class PedidoComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
export interface IPedido {
  id: number,
  codigo: number,
  carrito: ICarritoCompra
}
