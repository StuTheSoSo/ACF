import { Component, OnInit } from '@angular/core';
import { AuditLog } from 'src/app/models/auditLog';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-usage-report',
  templateUrl: './usage-report.component.html',
  styleUrls: ['./usage-report.component.css']
})
export class UsageReportComponent implements OnInit{
displayedColumns: string[] = ['message', 'level', 'timeStamp', 'exception', 'caseId', 'clientName', 'userName'];
auditLogs: AuditLog[] = [];

constructor(private dataService: DataService){}

ngOnInit(): void {
  this.dataService.getData('Reports/getUsage').subscribe({
      next: (response) => {
        this.auditLogs = response;
      },
      error: (err) => {
        console.error('err: ', err);
      }
    });
}

}
