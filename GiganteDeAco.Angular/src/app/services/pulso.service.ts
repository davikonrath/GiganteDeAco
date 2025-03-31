import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Lado } from '../enums/lado';
import { ObterRoboResponse } from '../interfaces/obterRoboResponse';

@Injectable({
  providedIn: 'root'
})
export class PulsoService {

  endpoint: string = environment.API_URL + environment.PULSO_BASE_URL;

  constructor(private http: HttpClient) { }

  avancarRotacaoPulso(lado: Lado) {
    const params = new HttpParams()
      .set('Lado', lado);

    return this.http.put<ObterRoboResponse>(this.endpoint + '/rotacao/avancar', null, { params });
  }

  voltarRotacaoPulso(lado: Lado) {
    const params = new HttpParams()
      .set('Lado', lado);

    return this.http.put<ObterRoboResponse>(this.endpoint + '/rotacao/voltar', null, { params })
  }
}
