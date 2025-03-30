import { Component, input, OnInit, output } from '@angular/core';
import { Lado } from '../../enums/lado';
import { PulsoService } from '../../services/pulso.service';
import { catchError, debounceTime, EMPTY, finalize, map, Observable, of, shareReplay, startWith, tap } from 'rxjs';
import { AsyncPipe, CommonModule, NgIf } from '@angular/common';
import { BracoDto } from '../../models/bracoDto';
import { RotacaoPulso } from '../../enums/rotacaoPulso';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'pulso',
  imports: [NgIf, AsyncPipe],
  templateUrl: './pulso.component.html',
  styleUrl: './pulso.component.css',
})
export class PulsoComponent implements OnInit{

  constructor(private pulsoService: PulsoService, private toastr: ToastrService){ }

  lado = input.required<Lado>();
  braco = input.required<BracoDto>();

  braco$!: Observable<BracoDto>;

  attRobo = output();

  ngOnInit() {
    this.braco$ = of(this.braco());
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

  avancarRotacaoPulso(){
    this.braco$ = this.pulsoService.avancarRotacaoPulso(this.lado())
      .pipe(
        map(response => this.lado() == Lado.Direito ? response.robo.bracoDireito : response.robo.bracoEsquerdo),
        catchError((err, caught) => {
          console.error('Erro:', err.error.notificacoes[0].mensagem); //ATUALIZAR PARA NOTIFICACAO
          this.attRobo.emit()
          this.toastr.error(err.error.notificacoes[0].mensagem)
          return caught;
        }),
      )
  }

  voltarRotacaoPulso(){
    this.braco$ = this.pulsoService.voltarRotacaoPulso(this.lado()).pipe(
      map(response => this.lado() == Lado.Direito ? response.robo.bracoDireito : response.robo.bracoEsquerdo),
      catchError((err) => {
        console.error('Erro:', err.error.notificacoes[0].mensagem); //ATUALIZAR PARA NOTIFICACAO
        this.attRobo.emit()
        this.toastr.error(err.error.notificacoes[0].mensagem)
        return EMPTY;
      }),
    )
  }
}
