import { Component, input, OnInit, output } from '@angular/core';
import { catchError, EMPTY, map, Observable, of } from 'rxjs';
import { BracoDto } from '../../models/bracoDto';
import { Lado } from '../../enums/lado';
import { CotoveloService } from '../../services/cotovelo.service';
import { AsyncPipe, NgIf } from '@angular/common';
import { ContracaoCotovelo } from '../../enums/contracaoCotovelo';

@Component({
  selector: 'cotovelo',
  imports: [NgIf, AsyncPipe],
  templateUrl: './cotovelo.component.html',
  styleUrl: './cotovelo.component.css'
})
export class CotoveloComponent implements OnInit {

  constructor(private cotoveloService: CotoveloService) { }

  lado = input.required<Lado>();
  braco = input.required<BracoDto>();

  braco$!: Observable<BracoDto>;
  anguloContracao: number = 0;

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

  transformCotovelo(braco: BracoDto) {
    switch (braco.contracaoCotovelo) {
      case ContracaoCotovelo.EmRepouso:
        this.anguloContracao = 15;
        break;
      case ContracaoCotovelo.Leve:
        this.anguloContracao = 80;
        break;
      case ContracaoCotovelo.Normal:
        this.anguloContracao = 120;
        break;
      case ContracaoCotovelo.Forte:
        this.anguloContracao = 160;
        break;
    }

    if(this.lado() === Lado.Direito)
      return `rotateX(${this.anguloContracao}deg) rotateY(0deg) rotateZ(-25deg)`
      
    
    return `rotateX(${this.anguloContracao}deg) rotateY(0deg) rotateZ(25deg)`
  }

  avancarContracaoCotovelo() {
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

  voltarContracaoCotovelo() {
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
