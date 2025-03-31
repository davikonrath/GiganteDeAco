import { NgIf } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Lado } from '../../enums/lado';
import { RotacaoPulso } from '../../enums/rotacaoPulso';
import { ObterRoboResponse } from '../../interfaces/obterRoboResponse';
import { BracoDto } from '../../models/bracoDto';
import { PulsoService } from '../../services/pulso.service';

@Component({
  selector: 'pulso',
  imports: [NgIf],
  templateUrl: './pulso.component.html',
  styleUrl: './pulso.component.css',
})
export class PulsoComponent {

  constructor(private pulsoService: PulsoService, private toastr: ToastrService) { }

  lado = input.required<Lado>();
  braco = input.required<BracoDto>();

  anguloRotacao: number = 0;

  attRobo = output();
  attBraco = output<Observable<ObterRoboResponse>>();

  transformPulso(braco: BracoDto) {
    switch (braco.rotacaoPulso) {
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

    return `rotateZ(${this.anguloRotacao}deg)`
  }

  obterDescricao(e: RotacaoPulso): string {
    switch (e) {
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
    var result = this.pulsoService.avancarRotacaoPulso(this.lado());

    this.attBraco.emit(result);
  }

  voltarRotacaoPulso() {
    var result = this.pulsoService.voltarRotacaoPulso(this.lado())

    this.attBraco.emit(result);
  }
}
