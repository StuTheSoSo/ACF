import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root', // Makes it a singleton, app-wide service
})
export class DataService {
  private apiUrl = 'https://localhost:7180/'; // Replace with your .NET API endpoint

  constructor(private http: HttpClient) {} // Inject HttpClient

  getData(endpoint: string): Observable<any> {
    return this.http.get<any>(this.apiUrl + endpoint); // Simple GET request
  }

  postData(endpoint: string, data: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + endpoint, data);
  }
}
