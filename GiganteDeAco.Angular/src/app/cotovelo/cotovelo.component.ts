import { Component, input, OnInit, output } from '@angular/core';
import { catchError, EMPTY, map, Observable, of } from 'rxjs';
import { BracoDto } from '../models/bracoDto';
import { Lado } from '../enums/lado';
import { CotoveloService } from '../services/cotovelo.service';
import { AsyncPipe, NgIf } from '@angular/common';
import { ContracaoCotovelo } from '../enums/contracaoCotovelo';

@Component({
  selector: 'cotovelo',
  imports: [NgIf, AsyncPipe],
  templateUrl: './cotovelo.component.html',
  styleUrl: './cotovelo.component.css'
})
export class CotoveloComponent implements OnInit {
  
  constructor(private cotoveloService: CotoveloService){ }

  lado = input.required<Lado>();
  braco = input.required<BracoDto>();

  braco$!: Observable<BracoDto>;

  attRobo = output();

  ngOnInit() {
    this.braco$ = of(this.braco());
  }

  obterDescricao(e: ContracaoCotovelo): string {
    switch (e) {
      case ContracaoCotovelo.EmRepouso:
        return "Em Repouso";
      case ContracaoCotovelo.Leve:
        return "Contração Leve";
      case ContracaoCotovelo.Normal:
        return "Contração Normal";
      case ContracaoCotovelo.Forte:
        return "Contração Forte";
      default:
        return "Contração Inválida";
    }
  }

  avancarContracaoCotovelo(){
    this.braco$ = this.cotoveloService.avancarContracaoCotovelo(this.lado())
      .pipe(
        map(response => this.lado() == Lado.Direito ? response.robo.bracoDireito : response.robo.bracoEsquerdo),
        catchError((err) => {
          console.error('Erro:', err.error.notificacoes[0].mensagem); //ATUALIZAR PARA NOTIFICACAO
          this.attRobo.emit()
          return EMPTY;
        }),
      )
  }

  voltarContracaoCotovelo(){
    this.braco$ = this.cotoveloService.voltarContracaoCotovelo(this.lado())
      .pipe(
        map(response => this.lado() == Lado.Direito ? response.robo.bracoDireito : response.robo.bracoEsquerdo),
        catchError((err) => {
          console.error('Erro:', err.error.notificacoes[0].mensagem); //ATUALIZAR PARA NOTIFICACAO
          this.attRobo.emit()
          return EMPTY;
        }),
      )
  } 
}
