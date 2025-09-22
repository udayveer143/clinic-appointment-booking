import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';


bootstrapApplication(AppComponent, {
  providers: [
    provideRouter([
      { path: '', redirectTo: 'appointments', pathMatch: 'full' },
      { path: 'appointments', loadComponent: () => import('./app/appointments/appointment-list/appointment-list.component').then(m => m.AppointmentListComponent) },
      { path: 'appointments/create', loadComponent: () => import('./app/appointments/appointment-form/appointment-form.component').then(m => m.AppointmentFormComponent) },
      { path: 'appointments/edit/:id', loadComponent: () => import('./app/appointments/appointment-form/appointment-form.component').then(m => m.AppointmentFormComponent) }
    ]),
    provideHttpClient(),
    importProvidersFrom(ReactiveFormsModule)
  ]
}).catch(err => console.error(err));


