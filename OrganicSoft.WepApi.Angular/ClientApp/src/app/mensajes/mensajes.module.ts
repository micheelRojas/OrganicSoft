import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';


@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class MensajesModule {
  mensajeAlertaCorrecto(titulo: string, texto: string) {
    Swal.fire({
      icon: 'success',
      title: titulo,
      text: texto,
    });
  }
  mensajeAlertaError(titulo: string, texto: string) {
    Swal.fire({
      icon: 'error',
      title: titulo,
      text: texto,
    });
  }
}
