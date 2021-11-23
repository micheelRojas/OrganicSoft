import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { IComponente, IProducto, IProductoCombo } from '../producto.component';
import { ProductoService } from '../producto.service';
import { Location } from '@angular/common';
@Component({
  selector: 'app-form-producto-combo',
  templateUrl: './form-producto-combo.component.html',
  styleUrls: ['./form-producto-combo.component.css']
})
export class FormProductoComboComponent implements OnInit {
  componentes: IComponente[] = [];
  constructor(private fb: FormBuilder, private productoService: ProductoService,
    private router: Router, private activatedRoute: ActivatedRoute, private mensaje: MensajesModule, private location: Location) { }
  formGroup = this.fb.group({
    codigoProducto: ['', [Validators.required]],
    nombre: ['', [Validators.required]],
    descripcion: ['', [Validators.required]],
    precio: ['', [Validators.required]],
    categoria: ['', [Validators.required]],
    presentacion: ['', [Validators.required]],
    minimoStock: ['', [Validators.required]],
   

  });

  ngOnInit() {
  }
  save() {
    let producto: IProductoCombo = Object.assign({}, this.formGroup.value);
    producto.componentes = this.componentes;
    console.table(producto); //ver grado por consola
    if (this.formGroup.valid) {
      this.productoService.CreateProductoCombo(producto)
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

  agregar(componente: IComponente) {
    this.componentes.push(componente);
    console.log(componente);
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
}
