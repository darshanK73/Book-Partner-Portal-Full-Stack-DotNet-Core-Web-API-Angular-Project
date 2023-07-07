import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';
import { Publisher } from '../Models/publisher';

@Injectable({
  providedIn: 'root'
})
export class PublisherService {

  private baseUrl = environment.baseUrl;

  constructor(private http:HttpClient) { }

  getAllPublishers()
  {
    return this.http.get<Publisher[]>(`${this.baseUrl}/publishers`);
  }

  getAllOwnTitles() {
    return this.http.get<Title[]>(`${this.baseUrl}/titles`);
  }

  getAllAuthorsIds(){
    return this.http.get<string[]>(`${this.baseUrl}/authors/allId`)
  }
  
  // getAllAuthors(){
  //   return this.http.get<Author[]>(`${this.baseUrl}/authors`);
  // }

  // getAllEmployees(){
  //   return this.http.get<Employee[]>(`${this.baseUrl}/employees`);
  // }
}
