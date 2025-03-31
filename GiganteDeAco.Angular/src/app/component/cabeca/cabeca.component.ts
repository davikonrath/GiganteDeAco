import { AsyncPipe, NgIf } from '@angular/common';
import { Component, input, OnInit, output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError, EMPTY, map, Observable, of } from 'rxjs';
import { InclinacaoCabeca } from '../../enums/inclinacaoCabeca';
import { RotacaoCabeca } from '../../enums/rotacaoCabeca';
import { CabecaDto } from '../../models/cabecaDto';
import { CabecaService } from '../../services/cabeca.service';

@Component({
  selector: 'cabeca',
  imports: [NgIf, AsyncPipe],
  templateUrl: './cabeca.component.html',
  styleUrl: './cabeca.component.css'
})
export class CabecaComponent implements OnInit {

  constructor(private cabecaService: CabecaService, private toastr: ToastrService) { }

  cabeca = input.required<CabecaDto>();

  cabeca$!: Observable<CabecaDto>
  anguloInclinacao: number = 10;
  anguloRotacao: number = 0;

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

  transformCabeca(cabeca: CabecaDto) {
    switch (cabeca.inclinacao) {
      case InclinacaoCabeca.ParaCima:
        this.anguloInclinacao = 30;
        break;
      case InclinacaoCabeca.EmRepouso:
        this.anguloInclinacao = -10;
        break;
      case InclinacaoCabeca.ParaBaixo:
        this.anguloInclinacao = -40;
        break;
    }

    switch (cabeca.rotacao) {
      case RotacaoCabeca.MenosNoventa:
        this.anguloRotacao = -90;
        break;
      case RotacaoCabeca.MenosQuarentaCinco:
        this.anguloRotacao = -45;
        break;
      case RotacaoCabeca.EmRepouso:
        this.anguloRotacao = 0;
        break;
      case RotacaoCabeca.QuarentaCinco:
        this.anguloRotacao = 45;
        break;
      case RotacaoCabeca.Noventa:
        this.anguloRotacao = 90;
        break;
    }
    return `rotateX(${this.anguloInclinacao}deg) rotateY(${this.anguloRotacao}deg)`
  }

  avancarRotacaoCabeca() {
    this.cabeca$ = this.cabecaService.avancarRotacaoCabeca()
      .pipe(
        map(response => response.robo.cabeca),
        catchError((err) => {
          this.toastr.error(err.error.notificacoes[0].mensagem)
          this.attRobo.emit()
          return EMPTY;
        }),
      )
  }

  voltarRotacaoCabeca() {
    this.cabeca$ = this.cabecaService.voltarRotacaoCabeca()
      .pipe(
        map(response => response.robo.cabeca),
        catchError((err) => {
          this.toastr.error(err.error.notificacoes[0].mensagem)
          this.attRobo.emit()
          return EMPTY;
        }),
      )
  }

  avancarInclinacaoCabeca() {
    this.cabeca$ = this.cabecaService.avancarInclinacaoCabeca()
      .pipe(
        map(response => response.robo.cabeca),
        catchError((err) => {
          this.toastr.error(err.error.notificacoes[0].mensagem)
          this.attRobo.emit()
          return EMPTY;
        }),
      )
  }

  voltarInclinacaoCabeca() {
    this.cabeca$ = this.cabecaService.voltarInclinacaoCabeca()
      .pipe(
        map(response => response.robo.cabeca),
        catchError((err) => {
          this.toastr.error(err.error.notificacoes[0].mensagem)
          this.attRobo.emit()
          return EMPTY;
        }),
      )
  }
}
