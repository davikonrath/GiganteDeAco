import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PulsoComponent } from './pulso.component';

describe('PulsoComponent', () => {
  let component: PulsoComponent;
  let fixture: ComponentFixture<PulsoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PulsoComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(PulsoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
