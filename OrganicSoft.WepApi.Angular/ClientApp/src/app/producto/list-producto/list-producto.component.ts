import { EventEmitter, Output } from '@angular/core';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { ModalProductoComponent } from '../modal-producto/modal-producto.component';
import { IComponente, IProducto, IProductoEdit } from '../producto.component';
import { ProductoService } from '../producto.service';
import { Location } from '@angular/common';
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
  productoEdit!: IProductoEdit;
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
    public dialog: MatDialog, private location: Location) {
  }
  id: number;
  modoEdicion: boolean = false;
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  ngOnInit(): void {
    
    const segments: UrlSegment[] = this.activatedRoute.snapshot.url;

    console.log(segments[0].toString());
    if (segments[0].toString() == 'lista-productos') {
      this.modoEdicion = true;
    }
    this.ConsultarProductos();
  }
  ConsultarProductos() {
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
      if (result.toString() != "undefined") {
        this.addComponente.emit(this.componente);
      }
      console.log(result);
    });
  }
  editar(producto: IProducto) {
    this.producto = producto;
    this.openDialogEdit();
  }
  openDialogEdit() {
    const dialogRef = this.dialog.open(ModalProductoComponent, {
      width: '250px'
    });

    dialogRef.afterClosed().subscribe(result => {
      this.productoEdit = {
        cantidad: Number(result),
        id: this.producto.id
      };
      if (result.toString() != "undefined") {
        this.productoService.updateProducto(this.productoEdit)
          .subscribe(producto => this.goBack(),
            error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
      }
     
      console.log(result.toString());
    });
  }
  
  goBack(): void {
    this.location.back();
    this.mensaje.mensajeAlertaCorrecto('Exitoso!', 'Entrada  de producto realizada correctamente');
  }

}
