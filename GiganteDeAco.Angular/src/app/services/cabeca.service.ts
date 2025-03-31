import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { ObterRoboResponse } from '../interfaces/obterRoboResponse';

@Injectable({
  providedIn: 'root'
})
export class CabecaService {

  endpoint: string = environment.API_URL + environment.CABECA_BASE_URL;

  constructor(private http: HttpClient) { }

  avancarRotacaoCabeca() {
    return this.http.put<ObterRoboResponse>(this.endpoint + '/rotacao/avancar', {})
  }

  voltarRotacaoCabeca() {
    return this.http.put<ObterRoboResponse>(this.endpoint + '/rotacao/voltar', {})
  }

  avancarInclinacaoCabeca() {
    return this.http.put<ObterRoboResponse>(this.endpoint + '/inclinacao/avancar', {})
  }

  voltarInclinacaoCabeca() {
    return this.http.put<ObterRoboResponse>(this.endpoint + '/inclinacao/voltar', {})
  }
}
