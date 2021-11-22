import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormProductoComboComponent } from './form-producto-combo.component';

describe('FormProductoComboComponent', () => {
  let component: FormProductoComboComponent;
  let fixture: ComponentFixture<FormProductoComboComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormProductoComboComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormProductoComboComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
