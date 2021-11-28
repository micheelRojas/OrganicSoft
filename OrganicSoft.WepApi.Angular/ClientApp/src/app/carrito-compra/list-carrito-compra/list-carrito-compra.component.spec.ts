import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListCarritoCompraComponent } from './list-carrito-compra.component';

describe('ListCarritoCompraComponent', () => {
  let component: ListCarritoCompraComponent;
  let fixture: ComponentFixture<ListCarritoCompraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListCarritoCompraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListCarritoCompraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
