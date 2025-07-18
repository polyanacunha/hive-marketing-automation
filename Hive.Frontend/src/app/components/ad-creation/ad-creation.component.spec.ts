import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdCreationComponent } from './ad-creation.component';

describe('AdCreationComponent', () => {
  let component: AdCreationComponent;
  let fixture: ComponentFixture<AdCreationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdCreationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
