import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalDatosCarritoComponent } from './modal-datos-carrito.component';

describe('ModalDatosCarritoComponent', () => {
  let component: ModalDatosCarritoComponent;
  let fixture: ComponentFixture<ModalDatosCarritoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModalDatosCarritoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalDatosCarritoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
