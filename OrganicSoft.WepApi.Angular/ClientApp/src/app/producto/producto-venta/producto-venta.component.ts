import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';

import { ActivatedRoute, Router } from '@angular/router';
import { CarritoCompraComponent, IAggCarritoCompra, ICarritoCompra, IProductoVenta } from '../../carrito-compra/carrito-compra.component';
import { CarritoCompraService } from '../../carrito-compra/carrito-compra.service';

import { ModalDatosCarritoComponent } from '../../carrito-compra/modal-datos-carrito/modal-datos-carrito.component';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { ModalProductoComponent } from '../modal-producto/modal-producto.component';
import { IProducto } from '../producto.component';
import { ProductoService } from '../producto.service';

@Component({
  selector: 'app-producto-venta',
  templateUrl: './producto-venta.component.html',
  styleUrls: ['./producto-venta.component.css']
})
export class ProductoVentaComponent implements OnInit {
  productos: IProducto[] = [];
  carrito!: ICarritoCompra;
  aggCarrito!: IAggCarritoCompra;
  id: number;
  constructor(private productoService: ProductoService, private router: Router,
    private activatedRoute: ActivatedRoute, private mensaje: MensajesModule, public dialog: MatDialog, private carritoService: CarritoCompraService) { }
  filterProducto = '';
  codigo: number;
  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.id = parseInt(params["id"]);
    })
    this.ConsultarProductos();
  }
  ConsultarProductos() {
    this.productoService.getProductos()
      .subscribe(productos => this.productos = productos,
        error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
  }
  
  agg(codigo: number) {
    this.codigo = codigo;
    this.openDialog();
  }

  openDialog(): void {
    const dialogRefe = this.dialog.open(ModalProductoComponent, {
      width: '250px'
    });
    let aggregar: IAggCarritoCompra;
    dialogRefe.afterClosed().subscribe(result => {
      this.aggCarrito = {
        productoVenta:{
          cantidadVenta: Number(result),
          codigoProducto: this.codigo
        },
        id: this.id
        
      };
      
        console.table(this.aggCarrito);
      console.log(this.id);
      if (result.toString() != "undefined") {
        this.carritoService.addToCarrito(this.aggCarrito)
          .subscribe(producto => this.exitoso(),
            error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
      }
     
      console.log(result);
    });
  }
  exitoso(): void {
    this.mensaje.mensajeAlertaCorrecto('¡Exitoso!', 'Datos del carrito guardados correctamente');
  }

  

    
  
}
