import { Component, OnInit } from '@angular/core';
import { IPedido, IPedidoView } from '../pedido/pedido.component';

@Component({
  selector: 'app-factura',
  templateUrl: './factura.component.html',
  styleUrls: ['./factura.component.css']
})
export class FacturaComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
export interface IFactura {
  id: number,
  codigo: number,
  pedido: IPedidoView
}

export interface IFacturaView {
  id: number,
  codigo: number,
  fechaCreacion: String,
  cedulaCliente: String,
  totalPagar: number
}
