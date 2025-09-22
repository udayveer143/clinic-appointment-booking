import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AppointmentService } from '../../services/appointment.service';
import { Appointment } from '../../models/appointment.model';

@Component({
  selector: 'app-appointment-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './appointment-form.component.html',
  styleUrl: './appointment-form.component.css'
})
export class AppointmentFormComponent implements OnInit {
  appointmentForm!: FormGroup;
  appointmentId!: number;
  //apiError: string | null = null;   // ✅ store API error message
  isEdit = false;
  alertMessage: string | null = null;   // ✅ store API error message
  alertType: 'success' | 'danger' = 'success';

  constructor(
    private fb: FormBuilder,
    private service: AppointmentService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.appointmentForm = this.fb.group({
      patientName: ['', [Validators.required, Validators.maxLength(100)]],
      doctorName: ['', [Validators.required, Validators.maxLength(100)]],
      startTime: ['', Validators.required],
      endTime: ['', Validators.required]
    });
    this.appointmentId = Number(this.route.snapshot.paramMap.get('id'));
    this.isEdit = !!this.appointmentId;

    if (this.isEdit) {
      this.service.getAppointment(this.appointmentId).subscribe(a => this.appointmentForm.patchValue(a));
    }
  }
  // ✅ Getter methods for template access
  get patientName() { return this.appointmentForm.get('patientName'); }
  get doctorName() { return this.appointmentForm.get('doctorName'); }
  get startTime() { return this.appointmentForm.get('startTime'); }
  get endTime() { return this.appointmentForm.get('endTime'); }
  save() {
    debugger;
    if (this.appointmentForm.invalid) {
      this.alertMessage = 'Please fix validation errors.';
      this.alertType = 'danger';
      return;
    }

    const appt: Appointment = this.appointmentForm.value as Appointment;

    if (this.isEdit) {
      this.service.updateAppointment(this.appointmentId, appt).subscribe({
        next: () => {
          alert('Appointment updated successfully!');
          this.router.navigate(['/appointments'])
        },
        error: err => {
          // Case 1: API returns a single string (BadRequest("..."))
          if (typeof err.error === 'string') {
            this.alertMessage = err.error || 'Update failed.';
          }
          // Case 2: API returns an array of errors
          else if (Array.isArray(err.error)) {
            this.alertMessage = err.error.join(' ');
          } // Case 3: API returns ModelState-style object { field: [errors] }
          else if (typeof err.error === 'object') {
            let errors: string[] = [];
            // Check if err.error has "errors" property (ASP.NET Core standard)
            const errorObj = err.error.errors ? err.error.errors : err.error;
            for (const key in errorObj) {
              if (errorObj.hasOwnProperty(key)) {
                const value = errorObj[key];
                if (Array.isArray(value)) {
                  errors.push(...value);  // Add all messages
                } else if (typeof value === 'string') {
                  errors.push(value);     // Add string message
                }
              }
            }
            this.alertMessage = errors.join(' ');
          }
          else {
            this.alertMessage = "An unexpected error occurred.";
          }
          this.alertType = 'danger';
        }
      });
    } else {
      this.service.createAppointment(appt).subscribe({
        next: () => {
          alert('Appointment created successfully!');
          this.router.navigate(['/appointments'])
        },
        error: err => {
          // Case 1: API returns a single string (BadRequest("..."))
          if (typeof err.error === 'string') {
            this.alertMessage = err.error || 'Creation failed.';
          }
          // Case 2: API returns an array of errors
          else if (Array.isArray(err.error)) {
            this.alertMessage = err.error.join(' ');
          }
          // Case 3: API returns ModelState-style object { field: [errors] }
          else if (typeof err.error === 'object') {
            let errors: string[] = [];
            // Check if err.error has "errors" property (ASP.NET Core standard)
            const errorObj = err.error.errors ? err.error.errors : err.error;
            for (const key in errorObj) {
              if (errorObj.hasOwnProperty(key)) {
                const value = errorObj[key];
                if (Array.isArray(value)) {
                  errors.push(...value);  // Add all messages
                } else if (typeof value === 'string') {
                  errors.push(value);     // Add string message
                }
              }
            }
            this.alertMessage = errors.join(' ');
          }
          else {
            this.alertMessage = "An unexpected error occurred.";
          }
          this.alertType = 'danger';
        }
      });
    }
  }
}