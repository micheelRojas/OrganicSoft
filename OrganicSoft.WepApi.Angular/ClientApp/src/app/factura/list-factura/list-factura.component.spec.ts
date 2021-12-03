import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListFacturaComponent } from './list-factura.component';

describe('ListFacturaComponent', () => {
  let component: ListFacturaComponent;
  let fixture: ComponentFixture<ListFacturaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListFacturaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListFacturaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
