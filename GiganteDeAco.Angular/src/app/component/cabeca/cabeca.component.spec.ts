import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CabecaComponent } from './cabeca.component';

describe('CabecaComponent', () => {
  let component: CabecaComponent;
  let fixture: ComponentFixture<CabecaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CabecaComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(CabecaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
