import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import {Moment} from 'moment';

@Injectable()

export class PayrollService {
  
  http: HttpClient;
  baseUrl: string;
  positions: Position[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;

    this.http.get<Position[]>(this.baseUrl + 'api/Dictionary/Position').subscribe(result => {
      this.positions = result;
    }, error => {
      this.positions = [];
      console.error(error);
    });
  }
  public calculateSalary(node: Person, date: Moment) : Observable<number> {
    const  params = new  HttpParams()
            .set('personId', node.id.toString())
            .set('date', date.toString());
    return this.http.get<number>(this.baseUrl + 'api/Salary/Calculate');
  }
  public getPersons(): Observable<Person[]> {
    return this.http.get<Person[]>(this.baseUrl + 'api/Person/Graph');
  }
  public savePerson(newcamer: any) : Observable<number> {
    return this.http.put<number>(this.baseUrl + 'api/Person', newcamer);
  }
  public getPositionName(positionCode: number): string {
    var result = this.positions.find(e => e.code == positionCode);
    return result ? result.name : "Unknown";
  }
}

export interface Person {
  id: number;
  position: number;
  rate: number;
  staff: Person[];
}

export interface Position {
  code: number;
  name: string;
}
