import { EventEmitter, Output } from '@angular/core';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { ModalProductoComponent } from '../modal-producto/modal-producto.component';
import { IComponente, IProducto } from '../producto.component';
import { ProductoService } from '../producto.service';

@Component({
  selector: 'app-list-producto',
  templateUrl: './list-producto.component.html',
  styleUrls: ['./list-producto.component.css']
})
export class ListProductoComponent implements OnInit {

  @Output() addComponente = new EventEmitter<IComponente>();


  productos!: IProducto[];
  producto!: IProducto;
  componente!: IComponente;
  displayedColumns: string[] = [
    'id',
    'nombre',
    'descripcion',
    'costo',
    'precio',
    'cantidadExistente',
    'cantidadVendida',
    'opciones'
   ];
  dataSource = new MatTableDataSource<IProducto>(this.productos);
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  constructor(private productoService: ProductoService, private router: Router,
    private activatedRoute: ActivatedRoute, private mensaje: MensajesModule,
    public dialog: MatDialog) {
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
    this.ConsultarMensualidad();
  }
  ConsultarMensualidad() {
    this.productoService.getProductos()
      .subscribe(productos => this.dataSource.data = productos,
        error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
  }

  ejecutar(producto: IProducto) {
    this.producto = producto;
    this.openDialog();
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ModalProductoComponent, {
      width: '250px'
    });

    dialogRef.afterClosed().subscribe(result => {
      this.componente = {
        cantidad: Number(result),
        producto: this.producto
      };
      this.addComponente.emit(this.componente);
      console.log(result);
    });
  }

}
