import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ModalDatosCarritoComponent } from '../carrito-compra/modal-datos-carrito/modal-datos-carrito.component';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor( public dialog: MatDialog) { }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  crear() {
    const dialogRef = this.dialog.open(ModalDatosCarritoComponent, {
      width: '500px',

    });
    // this.router.navigate(["/datos-carrito"]);
  }
}
