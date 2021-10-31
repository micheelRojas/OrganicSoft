import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule  } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IProductoCrear } from '../producto.component';
import { ProductoService } from '../producto.service';

@Component({
  selector: 'app-form-producto',
  templateUrl: './form-producto.component.html',
  styleUrls: ['./form-producto.component.css']
})
export class FormProductoComponent implements OnInit {

  
  constructor(private fb: FormBuilder, private productoService: ProductoService,
    private router: Router, private activatedRoute: ActivatedRoute) { }
  formGroup = this.fb.group({
    tipoProducto: ['', [Validators.required]],
    codigoProducto: ['', [Validators.required]],
    nombre: ['', [Validators.required]],
    descripcion: ['', [Validators.required]],
    precio: ['', [Validators.required]],
    categoria: ['', [Validators.required]],
    presentacion: ['', [Validators.required]],
    minimoStock: ['', [Validators.required]],
    componentes: [''],
    costo: ['']

  });

  ngOnInit() {
    
  }
  save() {
    let producto: IProductoCrear = Object.assign({}, this.formGroup.value);
    producto.componentes = null;
    console.table(producto); //ver grado por consola
    if (this.formGroup.valid) {
      this.productoService.createProducto(producto)
        .subscribe(cliente => this.goBack(),
          error => console.log('Error'));
    } else {
      console.log('Error 2');
    }
  }
  goBack(): void { 
    console.log('Guardado')
  }
  get tipoProducto() {
    return this.formGroup.get('tipoProducto');
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
  get componentes() {
    return this.formGroup.get('componentes');
  }
  get costo() {
    return this.formGroup.get('costo');
  }

}
