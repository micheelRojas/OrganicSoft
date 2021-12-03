import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPedido } from '../pedido/pedido.component';
import { IFactura, IFacturaView } from './factura.component';

@Injectable({
  providedIn: 'root'
})
export class FacturaServiceService {
  apiURL = this.baseUrl + "api/Factura";
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  CreateFactura(factura: IFactura): Observable<IFactura> {
    return this.http.post<IFactura>(this.apiURL, factura);
  }
  GetFacturas(): Observable<IFacturaView[]> {
    return this.http.get<IFacturaView[]>(this.apiURL);
  }
}
