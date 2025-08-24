import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { HomeComponent } from './components/home.component/home.component';
import { LoginComponent } from './components/login/login.component/login.component';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from './components/register.component/register.component';
import { HeaderComponent } from './components/header.component/header.component';
import { SidebarComponent } from './components/sidebar.component/sidebar.component';
import { authInterceptor } from './interceptors/auth-interceptor';
import { NewCaseComponent } from './components/new-case.component/new-case.component';
import { NewClientComponent } from './components/new-client.component/new-client.component';

@NgModule({
  declarations: [
    App,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    NewCaseComponent,
    NewClientComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HeaderComponent,
    SidebarComponent
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor]))
  ],
  bootstrap: [App]
})
export class AppModule { }
