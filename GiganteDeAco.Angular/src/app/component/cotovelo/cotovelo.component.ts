import { AsyncPipe, NgIf } from '@angular/common';
import { Component, input, OnInit, output, viewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError, EMPTY, map, Observable, of } from 'rxjs';
import { ContracaoCotovelo } from '../../enums/contracaoCotovelo';
import { Lado } from '../../enums/lado';
import { RotacaoPulso } from '../../enums/rotacaoPulso';
import { ObterRoboResponse } from '../../interfaces/obterRoboResponse';
import { BracoDto } from '../../models/bracoDto';
import { CotoveloService } from '../../services/cotovelo.service';
import { PulsoComponent } from '../pulso/pulso.component';

@Component({
  selector: 'cotovelo',
  imports: [NgIf, AsyncPipe, PulsoComponent],
  templateUrl: './cotovelo.component.html',
  styleUrl: './cotovelo.component.css'
})
export class CotoveloComponent implements OnInit {

  constructor(private cotoveloService: CotoveloService, private toastr: ToastrService) { }

  readonly pulso = viewChild.required<PulsoComponent>("pulso");
  readonly lado = input.required<Lado>();
  readonly braco = input.required<BracoDto>();

  braco$!: Observable<BracoDto>;
  anguloContracao: number = 0;

  attRobo = output();

  ngOnInit() {
    this.braco$ = of(this.braco());
  }

  atualizarRobo() {
    this.attRobo.emit()
  }

  atualizarBraco(braco: Observable<ObterRoboResponse>) {
    this.braco$ = braco.pipe(
      map(response => this.lado() == Lado.Direito ? response.robo.bracoDireito : response.robo.bracoEsquerdo),
      catchError((err) => {
        this.toastr.error(err.error.notificacoes[0].mensagem);
        this.attRobo.emit();
        return EMPTY;
      }),
    )
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

  obterRotacao(e: RotacaoPulso) {
    return this.pulso().obterDescricao(e);
  }

  avancarRotacaoPulso() {
    this.pulso().avancarRotacaoPulso();
  }

  voltarRotacaoPulso() {
    this.pulso().voltarRotacaoPulso();
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

    if (this.lado() === Lado.Direito)
      return `rotateX(${this.anguloContracao}deg) rotateY(0deg) rotateZ(-25deg)`


    return `rotateX(${this.anguloContracao}deg) rotateY(0deg) rotateZ(25deg)`
  }

  avancarContracaoCotovelo() {
    this.braco$ = this.cotoveloService.avancarContracaoCotovelo(this.lado())
      .pipe(
        map(response => this.lado() == Lado.Direito ? response.robo.bracoDireito : response.robo.bracoEsquerdo),
        catchError((err) => {
          this.toastr.error(err.error.notificacoes[0].mensagem)
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
          this.toastr.error(err.error.notificacoes[0].mensagem)
          this.attRobo.emit()
          return EMPTY;
        }),
      )
  }
}
