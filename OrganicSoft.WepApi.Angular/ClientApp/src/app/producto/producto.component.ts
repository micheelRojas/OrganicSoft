import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-producto',
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.css']
})
export class ProductoComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
export interface IProductoCrear {
  id: number,
  codigoProducto: number,
  nombre: string,
  descripcion: string,
  precio: number,
  categoria: string,
  presentacion: string,
  minimoStock: number,
  componentes: IComponete[],
  costo: number

}
export interface IComponete {
  producto: IProducto,
  cantidad: number
}
export interface IProducto {
  id: number,
  codigoProducto: number,
  nombre: string,
  descripcion: string,
  precio: number,
  categoria: string,
  presentacion: string,
  minimoStock: number,
  costo: number

}
