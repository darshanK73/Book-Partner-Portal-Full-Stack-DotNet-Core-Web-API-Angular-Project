import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TitleService {

  baseUrl:string = "https://localhost:7022/api/titles";
  constructor(private http:HttpClient) { }

  getAllTitles()
  {
    return this.http.get(this.baseUrl);
  }
}
