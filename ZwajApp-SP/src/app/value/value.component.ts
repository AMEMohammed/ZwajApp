import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {
Values:any;
  constructor(private http:HttpClient ) { }

  ngOnInit() {
    this.getValues()
  }
getValues()
{
  this.http.get('http://localhost:5000/api/values').subscribe(
    response=>{this.Values=response;},error=>{console.log(error);}
  )
}
}
 