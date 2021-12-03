import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAggCarritoCompra, ICarritoCompra, IViewContenido } from './carrito-compra.component';

@Injectable({
  providedIn: 'root'
})
export class CarritoCompraService {
  apiURL = this.baseUrl + "api/CarritoCompra";
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  CreateCarritoCompra(carrito: ICarritoCompra): Observable<ICarritoCompra> {
    return this.http.post<ICarritoCompra>(this.apiURL, carrito);
  }
  addToCarrito(producto: IAggCarritoCompra): Observable<IAggCarritoCompra> {
    return this.http.put<IAggCarritoCompra>(this.apiURL + '/add/', producto);
  }
  GetCarritos(): Observable<ICarritoCompra[]> {
    return this.http.get<ICarritoCompra[]>(this.apiURL);
  }
  GetContenidoCarritos(id: number): Observable<IViewContenido[]> {
    return this.http.get<IViewContenido[]>(this.apiURL + '/contenido/' + id);
  }

}
