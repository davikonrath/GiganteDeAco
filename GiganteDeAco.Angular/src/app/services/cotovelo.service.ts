import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Lado } from '../enums/lado';
import { ObterRoboResponse } from '../interfaces/obterRoboResponse';

@Injectable({
  providedIn: 'root'
})
export class CotoveloService {

  endpoint: string = environment.API_URL + environment.COTOVELO_BASE_URL;

  constructor(private http: HttpClient) { }

  avancarContracaoCotovelo(lado: Lado) {
    const params = new HttpParams()
      .set('Lado', lado);

    return this.http.put<ObterRoboResponse>(this.endpoint + '/contracao/avancar', null, { params })
  }

  voltarContracaoCotovelo(lado: Lado) {
    const params = new HttpParams()
      .set('Lado', lado);

    return this.http.put<ObterRoboResponse>(this.endpoint + '/contracao/voltar', null, { params })
  }
}
