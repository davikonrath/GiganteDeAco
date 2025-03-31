import { AsyncPipe, NgIf } from '@angular/common';
import { Component, input, OnInit, output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError, map, Observable, of } from 'rxjs';
import { Lado } from '../../enums/lado';
import { RotacaoPulso } from '../../enums/rotacaoPulso';
import { BracoDto } from '../../models/bracoDto';
import { PulsoService } from '../../services/pulso.service';

@Component({
  selector: 'pulso',
  imports: [NgIf, AsyncPipe],
  templateUrl: './pulso.component.html',
  styleUrl: './pulso.component.css',
})
export class PulsoComponent implements OnInit {

  constructor(private pulsoService: PulsoService, private toastr: ToastrService) { }

  lado = input.required<Lado>();
  braco = input.required<BracoDto>();

  bracoBackup!: BracoDto;

  braco$!: Observable<BracoDto>;
  rotatePulso$!: Observable<string>;

  anguloRotacao: number = 0;

  attRotacaoPulso = output<BracoDto>();

  ngOnInit() {
    this.braco$ = of(this.braco())
    this.transformPulso(this.braco$)
  }

  transformPulso(braco: Observable<BracoDto>) {
    this.rotatePulso$ = braco.pipe(map(braco => {
      this.bracoBackup = braco;

      switch (this.bracoBackup.rotacaoPulso) {
        case RotacaoPulso.MenosNoventa:
          this.anguloRotacao = -90;
          break;
        case RotacaoPulso.MenosQuarentaCinco:
          this.anguloRotacao = -45;
          break;
        case RotacaoPulso.EmRepouso:
          this.anguloRotacao = 0;
          break;
        case RotacaoPulso.QuarentaCinco:
          this.anguloRotacao = 45;
          break;
        case RotacaoPulso.Noventa:
          this.anguloRotacao = 90;
          break;
        case RotacaoPulso.CentoTrintaCinco:
          this.anguloRotacao = 135;
          break;
        case RotacaoPulso.CentoOitenta:
          this.anguloRotacao = 180;
          break;
      }

      this.attRotacaoPulso.emit(this.bracoBackup)
      return `rotateZ(${this.anguloRotacao}deg)`
    }));
  }

  avancarRotacaoPulso() {
    this.transformPulso(this.pulsoService.avancarRotacaoPulso(this.lado()).pipe(
      map(response => this.lado() == Lado.Direito ? response.robo.bracoDireito : response.robo.bracoEsquerdo),
      catchError((err) => {
        this.toastr.error(err.error.notificacoes[0].mensagem)
        return of(this.bracoBackup);
      }),
    ));
  }

  voltarRotacaoPulso() {
    this.transformPulso(this.pulsoService.voltarRotacaoPulso(this.lado()).pipe(
      map(response => this.lado() == Lado.Direito ? response.robo.bracoDireito : response.robo.bracoEsquerdo),
      catchError((err) => {
        this.toastr.error(err.error.notificacoes[0].mensagem)
        return of(this.bracoBackup)
      })));
  }
}