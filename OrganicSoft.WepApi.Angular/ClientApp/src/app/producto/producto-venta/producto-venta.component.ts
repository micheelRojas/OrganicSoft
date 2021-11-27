import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { IProducto } from '../producto.component';
import { ProductoService } from '../producto.service';

@Component({
  selector: 'app-producto-venta',
  templateUrl: './producto-venta.component.html',
  styleUrls: ['./producto-venta.component.css']
})
export class ProductoVentaComponent implements OnInit {
  productos: IProducto[] = [];
  constructor(private productoService: ProductoService, private router: Router,
    private activatedRoute: ActivatedRoute, private mensaje: MensajesModule) { }

  ngOnInit() {
    this.ConsultarProductos();
  }
  ConsultarProductos() {
    this.productoService.getProductos()
      .subscribe(productos => this.productos = productos,
        error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
  }
}
