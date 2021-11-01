import { Component, OnInit } from '@angular/core';
import { IProducto } from '../producto.component';

@Component({
  selector: 'app-list-producto',
  templateUrl: './list-producto.component.html',
  styleUrls: ['./list-producto.component.css']
})
export class ListProductoComponent implements OnInit {
  producto!: IProducto[];
  displayedColumns: string[] = [
    'id',
    'estudiante',
    'mes',
    'anio',
    'valorMensualidad',
    //'descuentoMensualidad',
    'deuda',
    'estado',
    // 'totalMensualidad',
    'correo',
    'options'];
  constructor() { }

  ngOnInit() {
  }

}
