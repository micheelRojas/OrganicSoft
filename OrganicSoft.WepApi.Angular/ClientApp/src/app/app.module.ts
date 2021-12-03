import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';

import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ProductoComponent } from './producto/producto.component';
import { FormProductoComponent } from './producto/form-producto/form-producto.component';
import { ListProductoComponent } from './producto/list-producto/list-producto.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MensajesModule } from './mensajes/mensajes.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSortModule, MatTableModule } from '@angular/material';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EditProductoComponent } from './producto/edit-producto/edit-producto.component';
import { FormProductoComboComponent } from './producto/form-producto-combo/form-producto-combo.component';
import { ModalProductoComponent } from './producto/modal-producto/modal-producto.component';
import { CommonModule } from '@angular/common';
import { ProductoVentaComponent } from './producto/producto-venta/producto-venta.component';
import { FilterPipe } from './pipes/filter.pipe';
import { ModalDatosCarritoComponent } from './carrito-compra/modal-datos-carrito/modal-datos-carrito.component';
import { CarritoCompraComponent } from './carrito-compra/carrito-compra.component';
import { ListCarritoCompraComponent } from './carrito-compra/list-carrito-compra/list-carrito-compra.component';
import { PedidoComponent } from './pedido/pedido.component';
import { ContedidoCarritoComponent } from './carrito-compra/contedido-carrito/contedido-carrito.component';
import { ModalCodigoComponent } from './carrito-compra/modal-codigo/modal-codigo.component';
import { ListPedidoComponent } from './pedido/list-pedido/list-pedido.component';
import { FacturaComponent } from './factura/factura.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ProductoComponent,
    FormProductoComponent,
    ListProductoComponent,
    EditProductoComponent,
    FormProductoComboComponent,
    ModalProductoComponent,
    ProductoVentaComponent,
    FilterPipe,
    ModalDatosCarritoComponent,
    CarritoCompraComponent,
    ListCarritoCompraComponent,
    PedidoComponent,
    ContedidoCarritoComponent,
    ModalCodigoComponent,
    ListPedidoComponent
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'registrar-producto', component: FormProductoComponent },
      { path: 'lista-productos', component: ProductoComponent },
      { path: 'entrada-productos', component: EditProductoComponent },
      { path: 'registrar-producto-combo', component: FormProductoComboComponent },
      { path: 'productos-venta/:id', component: ProductoVentaComponent },
      { path: 'datos-carrito', component: ModalDatosCarritoComponent },
      { path: 'lista-carritos', component: CarritoCompraComponent },
      { path: 'contenido-carrito/:id', component: ContedidoCarritoComponent },
      { path: 'lista-pedidos', component: PedidoComponent },
      
    ]),
    MensajesModule,
    BrowserAnimationsModule,
    MatSortModule,
    MatTableModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [ModalProductoComponent, ModalCodigoComponent],

  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
