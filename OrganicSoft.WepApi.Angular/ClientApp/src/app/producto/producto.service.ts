import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { IProducto, IProductoCrear } from './producto.component';
import { tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class ProductoService {
  apiURL = this.baseUrl + "api/Producto";
  private _refresh$ = new Subject<void>();
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  get refresh$() {
    return this._refresh$;
  }
  getProductos(): Observable<IProducto[]> {
    return this.http.get<IProducto[]>(this.apiURL);
  }
  createProducto(producto: IProductoCrear): Observable<IProductoCrear> {
    return this.http.post<IProductoCrear>(this.apiURL, producto);
  }

}

