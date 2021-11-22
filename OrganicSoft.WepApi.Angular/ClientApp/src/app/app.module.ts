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
import { EditProductoComponent } from './producto/edit-producto/edit-producto.component';
import { FormProductoComboComponent } from './producto/form-producto-combo/form-producto-combo.component';
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
    FormProductoComboComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
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
    ]),
    MensajesModule,
    BrowserAnimationsModule,
    MatSortModule,
    MatTableModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
