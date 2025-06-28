import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Step2SegmentComponent } from './step2-segment.component';

describe('Step2SegmentComponent', () => {
  let component: Step2SegmentComponent;
  let fixture: ComponentFixture<Step2SegmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Step2SegmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Step2SegmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
