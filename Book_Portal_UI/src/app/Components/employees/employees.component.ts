import { Component } from '@angular/core';
import { Employee } from 'src/app/Models/employee';
import { AuthorService } from 'src/app/Services/author.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent {

  employees:Employee[] = [];

  constructor(private authorService:AuthorService){}
  
  ngOnInit(): void {
    this.authorService.getAllEmployees().subscribe({next:(res)=> {
      this.employees = res;
      console.log(this.employees);
    },error:(err)=>{
      console.log(err);
    }});
  }

}
