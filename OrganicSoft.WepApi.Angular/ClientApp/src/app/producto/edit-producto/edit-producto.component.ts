import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule   } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { IProducto, IProductoEdit } from '../producto.component';
import { ProductoService } from '../producto.service';
import { Location } from '@angular/common';
@Component({
  selector: 'app-edit-producto',
  templateUrl: './edit-producto.component.html',
  styleUrls: ['./edit-producto.component.css']
})
export class EditProductoComponent implements OnInit {
  ListaProductos: IProducto[] = [];
  constructor(private fb: FormBuilder, private productoService: ProductoService,
    private router: Router, private activatedRoute: ActivatedRoute, private mensaje: MensajesModule, private location: Location) { }
  formGroup = this.fb.group({
    id: ['', [Validators.required]],
    cantidad: ['', [Validators.required]]
  });

  ngOnInit() {
    this.productoService.getProductos().subscribe(producto => this.LLenarProductos(producto),
      error => this.mensaje.mensajeAlertaError('Error', error.error.toString())); 
  }
  LLenarProductos(productos: IProducto[]) {
    this.ListaProductos = productos;
  }
  save() {
    let producto: IProductoEdit = Object.assign({}, this.formGroup.value);
    this.productoService.updateProducto(producto)
      .subscribe(producto => this.goBack(),
        error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
  } 
  goBack(): void {
    this.location.back();
    this.mensaje.mensajeAlertaCorrecto('Exitoso!', 'Entrada  de producto realizada correctamente');
  }
  get id() {
    return this.formGroup.get('id');
  }
  get cantidad() {
    return this.formGroup.get('cantidad');
  }

}
