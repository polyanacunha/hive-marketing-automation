import { Routes } from '@angular/router';
import { HomeComponent } from '../components/home/home.component';
import { AdCreationComponent } from './components/ad-creation/ad-creation.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'ad-creation', component: AdCreationComponent}
];
