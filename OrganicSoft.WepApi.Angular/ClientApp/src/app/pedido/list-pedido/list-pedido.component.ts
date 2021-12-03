import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalCodigoComponent } from '../../carrito-compra/modal-codigo/modal-codigo.component';
import { FacturaServiceService } from '../../factura/factura-service.service';
import { IFactura, IFacturaView } from '../../factura/factura.component';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { ModalProductoComponent } from '../../producto/modal-producto/modal-producto.component';
import { IPedido, IPedidoView } from '../pedido.component';
import { PedidoService } from '../pedido.service';

@Component({
  selector: 'app-list-pedido',
  templateUrl: './list-pedido.component.html',
  styleUrls: ['./list-pedido.component.css']
})
export class ListPedidoComponent implements OnInit {
  pedidos!: IPedidoView[];
  pedido!: IPedidoView;
  factura!: IFactura;
  displayedColumns: string[] = [
    'id',
    'codigoPedido',
    'estado',
    'opciones'
  ];
  dataSource = new MatTableDataSource<IPedidoView>(this.pedidos);
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  constructor(private pedidoService: PedidoService, private router: Router,
    private activatedRoute: ActivatedRoute, private mensaje: MensajesModule, public dialog: MatDialog, private facturaService: FacturaServiceService) {
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
    this.pedidoService.GetPedidos()
      .subscribe(pedidos => this.dataSource.data = pedidos,
        error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
  }

  confirmar(pedido: IPedidoView) {
    this.pedido = pedido;
    this.openDialog();
    
  }

  openDialog(): void {
    const dialogRefe = this.dialog.open(ModalCodigoComponent, {
      width: '250px'
    });

    dialogRefe.afterClosed().subscribe(result => {
      this.factura = {
        pedido: {
          codigoPedido: this.pedido.codigoPedido,
          id: this.pedido.id,
          estado: this.pedido.estado,
          carritoId: this.pedido.carritoId
         
        },
        codigo: Number(result),
        id: 0

      };

      console.table(this.factura);
      if (this.factura.codigo > 0) {
        this.facturaService.CreateFactura(this.factura)
          .subscribe(factura => this.exitoso(),
            error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
      }

      console.log(result);
    });
  }


  exitoso(): void {
    this.mensaje.mensajeAlertaCorrecto('¡Exitoso!', 'Pedido confirmado correctamente');
  }
}
