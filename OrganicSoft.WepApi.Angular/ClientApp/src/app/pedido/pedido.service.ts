import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPedido, IPedidoView } from './pedido.component';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {
  apiURL = this.baseUrl + "api/Pedido";
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  CreatePedido(pedido: IPedido): Observable<IPedido> {
    return this.http.post<IPedido>(this.apiURL, pedido);
  }
  GetPedidos(): Observable<IPedidoView[]> {
    return this.http.get<IPedidoView[]>(this.apiURL);
  }
}
