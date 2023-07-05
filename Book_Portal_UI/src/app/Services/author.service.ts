import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Author } from '../Models/author';
import { Employee } from '../Models/employee';
import { Title } from '../Models/titles';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  baseUrl:string = "https://localhost:7022/api";
  constructor(private http:HttpClient) { }

  getAllTitles()
  {
    return this.http.get<Title[]>(`${this.baseUrl}/titles`);
  }

  getAllAuthors(){
    return this.http.get<Author[]>(`${this.baseUrl}/authors`);
  }

  getAllEmployees(){
    return this.http.get<Employee[]>(`${this.baseUrl}/employees`);
  }
}
