import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContedidoCarritoComponent } from './contedido-carrito.component';

describe('ContedidoCarritoComponent', () => {
  let component: ContedidoCarritoComponent;
  let fixture: ComponentFixture<ContedidoCarritoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContedidoCarritoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContedidoCarritoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
