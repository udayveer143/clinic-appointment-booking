import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Appointment } from '../../models/appointment.model';
import { AppointmentService } from '../../services/appointment.service';

@Component({
  selector: 'app-appointment-list',
  imports: [CommonModule, RouterModule, FormsModule],
  standalone: true,
  templateUrl: './appointment-list.component.html',
  styleUrl: './appointment-list.component.css'
})
export class AppointmentListComponent implements OnInit {
  appointments: Appointment[] = [];
  filteredAppointments: Appointment[] = [];
  search = { patientName: '', doctorName: '', startDate: '', endDate: '' };

  constructor(private service: AppointmentService) {}

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments() {
    this.service.getAppointments().subscribe(data => {
      this.appointments = data;
      this.filteredAppointments = data;
    });
  }

  deleteAppointment(id: number) {
    if(confirm('Are you sure you want to delete this appointment?')) {
      this.service.deleteAppointment(id).subscribe(() => this.loadAppointments());
    }
  }

  filter() {
    this.filteredAppointments = this.appointments.filter(a => 
      (!this.search.patientName || a.patientName.toLowerCase().includes(this.search.patientName.toLowerCase())) &&
      (!this.search.doctorName || a.doctorName.toLowerCase().includes(this.search.doctorName.toLowerCase())) &&
      (!this.search.startDate || new Date(a.startTime) >= new Date(this.search.startDate)) &&
      (!this.search.endDate || new Date(a.endTime) <= new Date(this.search.endDate))
    );
  }
}
