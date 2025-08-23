import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { HomeComponent } from './components/home.component/home.component';
import { LoginComponent } from './components/login/login.component/login.component';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from './components/register.component/register.component';
import { HeaderComponent } from './components/header.component/header.component';
import { SidebarComponent } from './components/sidebar.component/sidebar.component';

@NgModule({
  declarations: [
    App,
    HomeComponent,
    LoginComponent,
    RegisterComponent
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
    provideHttpClient()
  ],
  bootstrap: [App]
})
export class AppModule { }
