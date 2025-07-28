import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { routes } from './app.routes';
import { MainLayoutComponent } from './components/main-layout/main-layout.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { CompanyOnboardingComponent } from './components/company-onboarding/company-onboarding.component';
import { AdCreationComponent } from './components/ad-creation/ad-creation.component';
import { DashboardModule } from './dashboard/dashboard.module';

@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    LoginComponent,
    RegisterComponent,
    CompanyOnboardingComponent,
    AdCreationComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    FormsModule,
    DashboardModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
