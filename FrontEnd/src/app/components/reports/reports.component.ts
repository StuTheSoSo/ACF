import { Component } from '@angular/core';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent {
  reportTypes: string[] = ['Case Reports', 'Usage Reports'];
  selectedReportType: string = '';

  reportTypeSelected(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.selectedReportType = selectElement.value;
  }

}
