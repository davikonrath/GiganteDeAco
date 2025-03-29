import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { ObterRoboResponse as ObterRoboResponse } from '../interfaces/obterRoboResponse';

@Injectable({ 
  providedIn: 'root' 
})
export class RoboService {

  endpoint: string = environment.API_URL;

  constructor(private http: HttpClient) { }

  obterRobo(){
    return this.http.get<ObterRoboResponse>(this.endpoint + '/robo')
  }

  resetarRobo(){
    return this.http.put<ObterRoboResponse>(this.endpoint + '/reset', null)
  }
}