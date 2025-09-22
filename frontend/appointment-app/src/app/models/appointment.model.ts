export interface Appointment {
  id: number;
  patientName: string;
  doctorName: string;
  startTime: string; // ISO string
  endTime: string;   // ISO string
}
