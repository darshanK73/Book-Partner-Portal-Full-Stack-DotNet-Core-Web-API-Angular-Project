import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Publisher } from '../Models/publisher';

@Injectable({
  providedIn: 'root'
})
export class PublisherService {

  baseUrl:string = "https://localhost:7022/api";
  constructor(private http:HttpClient) { }

  getAllPublishers()
  {
    return this.http.get<Publisher[]>(`${this.baseUrl}/publishers`);
  }

  // getAllAuthors(){
  //   return this.http.get<Author[]>(`${this.baseUrl}/authors`);
  // }

  // getAllEmployees(){
  //   return this.http.get<Employee[]>(`${this.baseUrl}/employees`);
  // }
}
