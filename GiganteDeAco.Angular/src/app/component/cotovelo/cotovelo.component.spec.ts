import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CotoveloComponent } from './cotovelo.component';

describe('CotoveloComponent', () => {
  let component: CotoveloComponent;
  let fixture: ComponentFixture<CotoveloComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CotoveloComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(CotoveloComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
