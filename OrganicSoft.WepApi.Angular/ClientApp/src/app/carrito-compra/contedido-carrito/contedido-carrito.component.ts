import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { IViewContenido } from '../carrito-compra.component';
import { CarritoCompraService } from '../carrito-compra.service';
import { Location } from '@angular/common';
@Component({
  selector: 'app-contedido-carrito',
  templateUrl: './contedido-carrito.component.html',
  styleUrls: ['./contedido-carrito.component.css']
})
export class ContedidoCarritoComponent implements OnInit {

  viewContenido!: IViewContenido[];
  displayedColumns: string[] = [
    'id',
    'nombre',
    'cantidad',
    'precioUnitario',
    'total'];
  dataSource = new MatTableDataSource<IViewContenido>(this.viewContenido);
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  constructor(private carritoService: CarritoCompraService, private router: Router,
    private activatedRoute: ActivatedRoute, private location: Location, private mensaje: MensajesModule) {
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
  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.id = parseInt(params["id"]);
    })
    this.ConsultarContenidoCarrito(this.id);
  }

  ConsultarContenidoCarrito(idt: number) {
    this.carritoService.GetContenidoCarritos(idt)
      .subscribe(viewContenidos => this.dataSource.data = viewContenidos,
        error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
  }

}
