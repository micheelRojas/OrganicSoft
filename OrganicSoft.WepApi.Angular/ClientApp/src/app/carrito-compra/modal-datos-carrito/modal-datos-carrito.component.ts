import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { Location } from '@angular/common';
import { ICarritoCompra } from '../carrito-compra.component';
import { CarritoCompraService } from '../carrito-compra.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-modal-datos-carrito',
  templateUrl: './modal-datos-carrito.component.html',
  styleUrls: ['./modal-datos-carrito.component.css']
})
export class ModalDatosCarritoComponent implements OnInit {

  constructor(private fb: FormBuilder, private carritoService: CarritoCompraService, private mensaje: MensajesModule, private router: Router, private activatedRoute: ActivatedRoute, private location: Location) { }
  formGroup = this.fb.group({
    codigo: ['', [Validators.required]],
    cedulaCliente: ['', [Validators.required]]
  });
  ngOnInit(): void {
  }
  save() {

    let carrito: ICarritoCompra = Object.assign({}, this.formGroup.value);
    console.table(carrito); //ver grado por consola
    if (this.formGroup.valid) {
      this.carritoService.CreateCarritoCompra(carrito)
        .subscribe(producto => this.exitoso(),
          error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
    } else {
      this.mensaje.mensajeAlertaError('Error', 'El formGroup de producto no es valido');
    }

  }
  exitoso(): void {
    this.mensaje.mensajeAlertaCorrecto('¡Exitoso!', 'Datos del carrito guardados correctamente');
   // this.location.back();

  }
  get codigo() {
    return this.formGroup.get('codigo');
  }
  get cedulaCliente() {
    return this.formGroup.get('cedulaCliente');
  }

}
