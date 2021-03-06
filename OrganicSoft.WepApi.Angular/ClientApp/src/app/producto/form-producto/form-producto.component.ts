import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule  } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { IProducto, IProductoCombo } from '../producto.component';
import { ProductoService } from '../producto.service';
import { Location } from '@angular/common';
@Component({
  selector: 'app-form-producto',
  templateUrl: './form-producto.component.html',
  styleUrls: ['./form-producto.component.css']
})
export class FormProductoComponent implements OnInit {

  
  constructor(private fb: FormBuilder, private productoService: ProductoService,
    private router: Router, private activatedRoute: ActivatedRoute, private mensaje: MensajesModule,private location: Location) { }
  formGroup = this.fb.group({
    codigoProducto: ['', [Validators.required]],
    nombre: ['', [Validators.required]],
    descripcion: ['', [Validators.required]],
    precio: ['', [Validators.required]],
    categoria: ['', [Validators.required]],
    presentacion: ['', [Validators.required]],
    minimoStock: ['', [Validators.required]],
    costo: ['']

  });

  ngOnInit() {
    
  }
  save() {
    let producto: IProducto = Object.assign({}, this.formGroup.value);
    console.table(producto); //ver grado por consola
    if (this.formGroup.valid) {
      this.productoService.CreateProductoSimple(producto)
        .subscribe(producto => this.goBack(),
          error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
    } else {
      this.mensaje.mensajeAlertaError('Error', 'El formGroup de producto no es valido');
    }
  }
  goBack(): void { 
    this.mensaje.mensajeAlertaCorrecto('¡Exitoso!', 'Producto guardado correctamente');
    this.location.back();
  }
  
  get codigoProducto() {
    return this.formGroup.get('codigoProducto');
  }
  get nombre() {
    return this.formGroup.get('nombre');
  }
  get descripcion() {
    return this.formGroup.get('descripcion');
  }
  get precio() {
    return this.formGroup.get('precio');
  }
  get categoria() {
    return this.formGroup.get('categoria');
  }
  get presentacion() {
    return this.formGroup.get('presentacion');
  }
  get minimoStock() {
    return this.formGroup.get('minimoStock');
  }
  get costo() {
    return this.formGroup.get('costo');
  }

}
