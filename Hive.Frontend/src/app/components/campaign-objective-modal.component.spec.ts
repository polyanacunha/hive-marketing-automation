import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CampaignObjectiveModalComponent } from './campaign-objective-modal.component';
import { FormsModule } from '@angular/forms';

describe('CampaignObjectiveModalComponent', () => {
  let component: CampaignObjectiveModalComponent;
  let fixture: ComponentFixture<CampaignObjectiveModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CampaignObjectiveModalComponent ],
      imports: [ FormsModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CampaignObjectiveModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
}); 