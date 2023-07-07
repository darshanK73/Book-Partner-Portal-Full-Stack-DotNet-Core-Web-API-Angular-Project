import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Author } from '../Models/author';
import { AuthorId } from '../Models/author-id';
import { Employee } from '../Models/employee';
import { MessageResponse } from '../Models/message-response';
import { Store } from '../Models/stores';
import { Title } from '../Models/title';
import {TitleRequest} from '../Models/title-request'
import { TitleResponse } from '../Models/title-response';
import { TitleUpdateRequest } from '../Models/title-update-request';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  private baseUrl = environment.baseUrl;

  constructor(private http:HttpClient) { }

  getAllTitles()
  {
    return this.http.get<Title[]>(`${this.baseUrl}/titles`);
  }

  getAllOwnTitles(authorId:string)
  {
    return this.http.get<Title[]>(`${this.baseUrl}/titles/authorId/${authorId}`)
  }

  getAllAuthors(){
    return this.http.get<Author[]>(`${this.baseUrl}/authors`);
  }

  getAllEmployees(){
    return this.http.get<Employee[]>(`${this.baseUrl}/employees`);
  }

  getAllStoresSellingTitle(titles:string[]) {
    return this.http.post<Store[]>(`${this.baseUrl}/stores/titlesIds`,titles);
  }

  postPublishTitles(title:TitleRequest) {
    return this.http.post<MessageResponse>(`${this.baseUrl}/titles`, title);
  }

  getTitleFromId(titleId:string)
  {
    return this.http.get<TitleResponse>(`${this.baseUrl}/titles/${titleId}`);
  }

  getAllAuthorsIds(){
    return this.http.get<AuthorId[]>(`${this.baseUrl}/authors/allId`)
  }

  updateTitleDetails(title:TitleUpdateRequest,titleId:string){
    return this.http.put<MessageResponse>(`${this.baseUrl}/titles/${titleId}`,title);
  }

}
