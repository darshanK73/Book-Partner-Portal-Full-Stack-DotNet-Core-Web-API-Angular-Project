import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Author } from '../Models/author';
import { AuthorId } from '../Models/author-id';
import { Employee } from '../Models/employee';
import { Store } from '../Models/stores';
import { Title } from '../Models/title';
import {TitleRequest} from '../Models/title-request'

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

  getAllStores() {
    return this.http.get<Store[]>(`${this.baseUrl}/stores`);
  }

  postPublishTitles(title:TitleRequest) {
    return this.http.post<string>(`${this.baseUrl}/titles`, title);
  }

  getTitleFromId(titleId:string)
  {
    return this.http.get<TitleRequest>(`${this.baseUrl}/titles/${titleId}`);
  }

  getAllAuthorsIds(){
    return this.http.get<AuthorId[]>(`${this.baseUrl}/authors/allId`)
  }
}
