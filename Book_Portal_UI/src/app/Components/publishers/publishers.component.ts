import { Component, OnInit } from '@angular/core';
import { Publisher } from 'src/app/Models/publisher';
import { PublisherService } from 'src/app/Services/publisher.service';

@Component({
  selector: 'app-publishers',
  templateUrl: './publishers.component.html',
  styleUrls: ['./publishers.component.css']
})
export class PublishersComponent implements OnInit {

  publishers:Publisher[] = [];

  constructor(private publisherService:PublisherService){}

  ngOnInit(): void {
    this.publisherService.getAllPublishers().subscribe({next:(res)=> {
      this.publishers = res;
      console.log(this.publishers);
    },error:(err)=>{
      console.log(err);
    }});
  }

}
