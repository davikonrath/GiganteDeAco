import { AsyncPipe, NgIf } from '@angular/common';
import { Component, input, OnInit, viewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError, map, Observable, of } from 'rxjs';
import { ContracaoCotovelo } from '../../enums/contracaoCotovelo';
import { Lado } from '../../enums/lado';
import { RotacaoPulso } from '../../enums/rotacaoPulso';
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
  rotateBraco$!: Observable<string>;
  descricaoContracao$!: Observable<string>;
  descricaoPulso$!: Observable<string>;

  anguloContracao: number = 0;
  bracoBackup!: BracoDto;

  ngOnInit() {
    this.braco$ = of(this.braco())
    this.transformCotovelo(this.braco$)
  }

  atualizarDescricaoPulso(braco: BracoDto) {
    this.descricaoPulso$ = of(this.obterDescricaoPulso(braco));
  }

  obterDescricao(braco: BracoDto): string {
    switch (braco.contracaoCotovelo) {
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

  obterDescricaoPulso(braco: BracoDto): string {
    switch (braco.rotacaoPulso) {
      case RotacaoPulso.EmRepouso:
        return "Em Repouso";
      case RotacaoPulso.MenosNoventa:
        return "-90°";
      case RotacaoPulso.MenosQuarentaCinco:
        return "-45°";
      case RotacaoPulso.QuarentaCinco:
        return "45°";
      case RotacaoPulso.Noventa:
        return "90°";
      case RotacaoPulso.CentoTrintaCinco:
        return "135°";
      case RotacaoPulso.CentoOitenta:
        return "180°";
      default:
        return "Rotação Inválida";
    }
  }

  avancarRotacaoPulso() {
    this.pulso().avancarRotacaoPulso();
  }

  voltarRotacaoPulso() {
    this.pulso().voltarRotacaoPulso();
  }

  transformCotovelo(braco: Observable<BracoDto>) {
    this.rotateBraco$ = braco.pipe(
      map((braco) => {
        this.bracoBackup = braco;

        switch (this.bracoBackup.contracaoCotovelo) {
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

        this.descricaoContracao$ = of(this.obterDescricao(this.bracoBackup));
        this.descricaoPulso$ = of(this.obterDescricaoPulso(this.bracoBackup));

        if (this.lado() === Lado.Direito)
          return `rotateX(${this.anguloContracao}deg) rotateY(0deg) rotateZ(25deg)`

        return `rotateX(${this.anguloContracao}deg) rotateY(0deg) rotateZ(-25deg)`
      }),
    );
  }

  avancarContracaoCotovelo() {
    this.transformCotovelo(this.cotoveloService.avancarContracaoCotovelo(this.lado())
      .pipe(
        map(response => this.lado() == Lado.Direito ? response.robo.bracoDireito : response.robo.bracoEsquerdo),
        catchError((err) => {
          this.toastr.error(err.error.notificacoes[0].mensagem)
          return of(this.bracoBackup);
        }),
      ))
  }

  voltarContracaoCotovelo() {
    this.transformCotovelo(this.cotoveloService.voltarContracaoCotovelo(this.lado())
      .pipe(
        map(response => this.lado() == Lado.Direito ? response.robo.bracoDireito : response.robo.bracoEsquerdo),
        catchError((err) => {
          this.toastr.error(err.error.notificacoes[0].mensagem)
          return of(this.bracoBackup);
        }),
      ))
  }
}
