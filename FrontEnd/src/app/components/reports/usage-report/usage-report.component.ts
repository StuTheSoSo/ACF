import { Component, OnInit } from '@angular/core';
import { AuditLog } from 'src/app/models/auditLog';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-usage-report',
  templateUrl: './usage-report.component.html',
  styleUrls: ['./usage-report.component.css']
})
export class UsageReportComponent implements OnInit {
  displayedColumns: string[] = ['message', 'level', 'timeStamp', 'exception', 'caseId', 'clientName', 'userName'];
  auditLogs: AuditLog[] = [];

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.dataService.getData('Reports/getUsage').subscribe({
      next: (response) => {
        this.auditLogs = response;
        console.log(this.auditLogs);
      },
      error: (err) => {
        console.error('err: ', err);
      }
    });
  }

  // Export to CSV
  exportToCsv(): void {
    const csvData = this.convertToCSV(this.auditLogs, this.displayedColumns);
    this.downloadCSV(csvData, 'table-data.csv');
  }

  private convertToCSV(objArray: any[], headerList: string[]): string {
    let csv = headerList.join(',') + '\n';

    objArray.forEach(row => {
      const values = headerList.map(field => JSON.stringify(row[field] ?? ''));
      csv += values.join(',') + '\n';
    });

    return csv;
  }

  private downloadCSV(csvData: string, filename: string) {
    const blob = new Blob([csvData], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);

    const link = document.createElement('a');
    link.href = url;
    link.setAttribute('download', filename);

    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);

    URL.revokeObjectURL(url); // cleanup
  }

  // Export to JSON
  exportToJson(): void {
    const data = this.auditLogs;

    // Create JSON content
    const jsonContent = JSON.stringify(data, null, 2);

    // Create a Blob and trigger download
    const blob = new Blob([jsonContent], { type: 'application/json;charset=utf-8;' });
    const link = document.createElement('a');
    const url = URL.createObjectURL(blob);
    link.setAttribute('href', url);
    link.setAttribute('download', 'table-data.json');
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }

}
