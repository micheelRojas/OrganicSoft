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
  productoVenta!: IProductoVenta;
  aggCarrito!: IAggCarritoCompra;
  constructor(private productoService: ProductoService, private router: Router,
    private activatedRoute: ActivatedRoute, private mensaje: MensajesModule, public dialog: MatDialog, private carritoService: CarritoCompraService) { }
  filterProducto = '';
  codigo: number;
  ngOnInit() {
    this.ConsultarProductos();
  }
  ConsultarProductos() {
    this.productoService.getProductos()
      .subscribe(productos => this.productos = productos,
        error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
  }
  crear() {
    const dialogRef = this.dialog.open(ModalDatosCarritoComponent, {
      width: '500px',
       
    });
   // this.router.navigate(["/datos-carrito"]);
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
      this.productoVenta = {
        cantidadVenta: Number(result),
        codigoProducto: this.codigo
      };
      console.table(this.productoVenta);
      this.aggCarrito.productoVenta = this.productoVenta;
      this.carritoService.addToCarrito(this.aggCarrito)
        .subscribe(producto => this.exitoso(),
          error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
      console.log(result);
    });
  }
  exitoso(): void {
    this.mensaje.mensajeAlertaCorrecto('¡Exitoso!', 'Datos del carrito guardados correctamente');
  }

  

    
  
}
