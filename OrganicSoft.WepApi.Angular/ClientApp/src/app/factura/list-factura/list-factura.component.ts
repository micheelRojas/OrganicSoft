import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { FacturaServiceService } from '../factura-service.service';
import { IFacturaView } from '../factura.component';

@Component({
  selector: 'app-list-factura',
  templateUrl: './list-factura.component.html',
  styleUrls: ['./list-factura.component.css']
})
export class ListFacturaComponent implements OnInit {

  facturas!: IFacturaView[];
  factura!: IFacturaView;
  //factura!: IFactura;
  displayedColumns: string[] = [
    'id',
    'codigo',
    'fechaCreacion',
    'cedulaCliente',
    'totalPagar'
  ];
  dataSource = new MatTableDataSource<IFacturaView>(this.facturas);
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  constructor(private facturaServiceService: FacturaServiceService, private router: Router,
    private activatedRoute: ActivatedRoute, private mensaje: MensajesModule, public dialog: MatDialog) {
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
    this.ConsultarPedidos();
  }
  ConsultarPedidos() {
    this.facturaServiceService.GetFacturas()
      .subscribe(facturas => this.dataSource.data = facturas,
        error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
  }

  //confirmar(pedido: IPedidoView) {
  //  this.pedido = pedido;
  //  this.openDialog();

  

  //openDialog(): void {
  //  const dialogRefe = this.dialog.open(ModalProductoComponent, {
  //    width: '250px'
  //  });

  //  dialogRefe.afterClosed().subscribe(result => {
  //    this.factura = {
  //      pedido: {
  //        codigoPedido: this.pedido.codigoPedido,
  //        id: this.pedido.id,
  //        estado: this.pedido.estado,
  //        carritoId: this.pedido.carritoId

  //      },
  //      codigo: Number(result),
  //      id: 0

  //    };

  //    console.table(this.factura);
  //    if (this.factura.codigo > 0) {
  //      this.facturaService.CreateFactura(this.factura)
  //        .subscribe(factura => this.exitoso(),
  //          error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
  //    }

  //    console.log(result);
  //  });
  //}


  exitoso(): void {
    this.mensaje.mensajeAlertaCorrecto('¡Exitoso!', 'Pedido confirmado correctamente');
  }

}
