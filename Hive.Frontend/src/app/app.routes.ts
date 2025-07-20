import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { CompanyOnboardingComponent } from './components/company-onboarding/company-onboarding.component';
import { AdCreationComponent } from './components/ad-creation/ad-creation.component';
import { MainLayoutComponent } from './components/main-layout/main-layout.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CreateCampaignModalComponent } from './components/create-campaign-modal/create-campaign-modal.component';

export const routes: Routes = [
  { 
    path: '', 
    component: MainLayoutComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent},
  { path: 'company-onboarding', component: CompanyOnboardingComponent },
  { path: 'ad-creation', component: AdCreationComponent},
  { path: 'create-campaign', component: CreateCampaignModalComponent },

];
