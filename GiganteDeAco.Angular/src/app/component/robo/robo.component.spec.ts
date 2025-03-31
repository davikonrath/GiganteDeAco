import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoboComponent } from './robo.component';

describe('RoboComponent', () => {
  let component: RoboComponent;
  let fixture: ComponentFixture<RoboComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoboComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(RoboComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
