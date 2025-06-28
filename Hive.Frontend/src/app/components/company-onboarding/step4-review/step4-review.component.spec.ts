import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Step4ReviewComponent } from './step4-review.component';

describe('Step4ReviewComponent', () => {
  let component: Step4ReviewComponent;
  let fixture: ComponentFixture<Step4ReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Step4ReviewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Step4ReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
