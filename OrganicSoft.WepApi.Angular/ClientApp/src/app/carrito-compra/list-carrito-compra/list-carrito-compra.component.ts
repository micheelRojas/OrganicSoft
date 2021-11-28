import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { ICarritoCompra } from '../carrito-compra.component';
import { CarritoCompraService } from '../carrito-compra.service';

@Component({
  selector: 'app-list-carrito-compra',
  templateUrl: './list-carrito-compra.component.html',
  styleUrls: ['./list-carrito-compra.component.css']
})
export class ListCarritoCompraComponent implements OnInit {
  carritos!: ICarritoCompra[];
  displayedColumns: string[] = [
    'id',
    'codigo',
    'cedulaCliente',
    'opciones'
  ];
  dataSource = new MatTableDataSource<ICarritoCompra>(this.carritos);
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  constructor(private carritoService: CarritoCompraService, private router: Router,
    private activatedRoute: ActivatedRoute, private mensaje: MensajesModule) {
  }
  id: number;
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  ngOnInit() {
    this.ColsultarCarritos();
  }
  ColsultarCarritos() {
    this.carritoService.GetCarritos()
      .subscribe(carritos => this.dataSource.data = carritos,
        error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
  }
 
  finalizar() { }
 

}
