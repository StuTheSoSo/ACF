import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router'; 
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { routes } from './app.routes';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { AuthInterceptor } from './interceptors/auth-interceptor';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { NewCaseComponent } from './components/new-case/new-case.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { NewClientComponent } from './components/new-client/new-client.component';
import { ReportsComponent } from './components/reports/reports.component';
import { AdminComponent } from './components/admin/admin.component';
import { UsageReportComponent } from './components/reports/usage-report/usage-report.component';
import { CaseReportComponent } from './components/reports/case-report/case-report.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    NewCaseComponent,
    NewClientComponent,
    ReportsComponent,
    AdminComponent,
    UsageReportComponent,
    CaseReportComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    RouterModule.forRoot(routes),
    HttpClientModule,
    HeaderComponent,
    SidebarComponent,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
