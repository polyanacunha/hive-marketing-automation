import { Routes } from '@angular/router';
import { HomeComponent } from '../components/home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { CompanyOnboardingComponent } from './components/company-onboarding/company-onboarding.component';
import { AdCreationComponent } from './components/ad-creation/ad-creation.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent},
  { path: 'company-onboarding', component: CompanyOnboardingComponent },
  { path: 'ad-creation', component: AdCreationComponent}
];
