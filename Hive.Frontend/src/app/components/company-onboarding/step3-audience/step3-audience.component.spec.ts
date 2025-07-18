import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Step3AudienceComponent } from './step3-audience.component';

describe('Step3AudienceComponent', () => {
  let component: Step3AudienceComponent;
  let fixture: ComponentFixture<Step3AudienceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Step3AudienceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Step3AudienceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
