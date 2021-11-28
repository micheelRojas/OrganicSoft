import { TestBed } from '@angular/core/testing';

import { CarritoCompraService } from './carrito-compra.service';

describe('CarritoCompraService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CarritoCompraService = TestBed.get(CarritoCompraService);
    expect(service).toBeTruthy();
  });
});
