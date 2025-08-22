import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root' // Makes it a singleton, app-wide service
})
export class DataService {
  private apiUrl = 'https://localhost:7180/WeatherForecast'; // Replace with your .NET API endpoint

  constructor(private http: HttpClient) {} // Inject HttpClient

  getData(): Observable<any> { // Returns an Observable of your data type (replace 'any' with your model, e.g., Observable<MyData[]>)
    console.log('DataService.getData() called');
    return this.http.get<any>(this.apiUrl); // Simple GET request
  }
}