import { Component, input, OnInit, output } from '@angular/core';
import { catchError, EMPTY, map, Observable, of } from 'rxjs';
import { CabecaDto } from '../../models/cabecaDto';
import { CabecaService } from '../../services/cabeca.service';
import { AsyncPipe, NgIf } from '@angular/common';
import { RotacaoCabeca } from '../../enums/rotacaoCabeca';
import { InclinacaoCabeca } from '../../enums/inclinacaoCabeca';

@Component({
  selector: 'cabeca',
  imports: [NgIf, AsyncPipe],
  templateUrl: './cabeca.component.html',
  styleUrl: './cabeca.component.css'
})
export class CabecaComponent implements OnInit{
  
  constructor(private cabecaService: CabecaService){ }
  
  cabeca = input.required<CabecaDto>();
  
  cabeca$!: Observable<CabecaDto>
  
  attRobo = output();

  ngOnInit() {
    this.cabeca$ = of(this.cabeca())
  }

  obterDescricaoRotacao(e: RotacaoCabeca): string {
    switch (e) {
      case RotacaoCabeca.EmRepouso:
        return "Em Repouso";
      case RotacaoCabeca.MenosNoventa:
        return "-90°";
      case RotacaoCabeca.MenosQuarentaCinco:
        return "-45°";
      case RotacaoCabeca.QuarentaCinco:
        return "45°";
      case RotacaoCabeca.Noventa:
        return "90°";
      default:
        return "Rotação Inválida";
    }
  }

  obterDescricaoInclinacao(e: InclinacaoCabeca): string {
    switch (e) {
      case InclinacaoCabeca.EmRepouso:
        return "Em Repouso";
      case InclinacaoCabeca.ParaCima:
        return "Para Cima";
      case InclinacaoCabeca.ParaBaixo:
        return "Para Baixo";
      default:
        return "Inclinação Inválida";
    }
  }

  avancarRotacaoCabeca(){
    this.cabeca$ = this.cabecaService.avancarRotacaoCabeca()
      .pipe(
        map(response => response.robo.cabeca),
        catchError((err) => {
          console.error('Erro:', err.error.notificacoes[0].mensagem); //ATUALIZAR PARA NOTIFICACAO
          this.attRobo.emit()
          return EMPTY;
        }),
    )
  }

  voltarRotacaoCabeca(){
    this.cabeca$ = this.cabecaService.voltarRotacaoCabeca()
      .pipe(
        map(response => response.robo.cabeca),
        catchError((err) => {
          console.error('Erro:', err.error.notificacoes[0].mensagem); //ATUALIZAR PARA NOTIFICACAO
          this.attRobo.emit()
          return EMPTY;
        }),
    )
  }

  avancarInclinacaoCabeca(){
    this.cabeca$ = this.cabecaService.avancarInclinacaoCabeca()
      .pipe(
        map(response => response.robo.cabeca),
        catchError((err) => {
          console.error('Erro:', err.error.notificacoes[0].mensagem); //ATUALIZAR PARA NOTIFICACAO
          this.attRobo.emit()
          return EMPTY;
        }),
    )
  }

  voltarInclinacaoCabeca(){
    this.cabeca$ = this.cabecaService.voltarInclinacaoCabeca()
      .pipe(
        map(response => response.robo.cabeca),
        catchError((err) => {
          console.error('Erro:', err.error.notificacoes[0].mensagem); //ATUALIZAR PARA NOTIFICACAO
          this.attRobo.emit()
          return EMPTY;
        }),
    )
  }
}
