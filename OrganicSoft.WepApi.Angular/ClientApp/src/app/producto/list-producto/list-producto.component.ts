import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { MensajesModule } from '../../mensajes/mensajes.module';
import { IProducto } from '../producto.component';
import { ProductoService } from '../producto.service';

@Component({
  selector: 'app-list-producto',
  templateUrl: './list-producto.component.html',
  styleUrls: ['./list-producto.component.css']
})
export class ListProductoComponent implements OnInit {
  producto!: IProducto[];
  displayedColumns: string[] = [
    'id',
    'nombre',
    'descripcion',
    'costo',
    'precio',
    'cantidadExistente',
    'cantidadVendida'
   ];
  dataSource = new MatTableDataSource<IProducto>(this.producto);
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  constructor(private productoService: ProductoService, private router: Router,
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
  ngOnInit(): void {
    this.ConsultarMensualidad();
  }
  ConsultarMensualidad() {
    this.productoService.getProductos()
      .subscribe(productos => this.dataSource.data = productos,
        error => this.mensaje.mensajeAlertaError('Error', error.error.toString()));
  }

}
