import { AsyncPipe, NgIf } from '@angular/common';
import { Component, input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError, map, Observable, of } from 'rxjs';
import { InclinacaoCabeca } from '../../enums/inclinacaoCabeca';
import { RotacaoCabeca } from '../../enums/rotacaoCabeca';
import { CabecaDto } from '../../models/cabecaDto';
import { CabecaService } from '../../services/cabeca.service';

@Component({
  selector: 'cabeca',
  imports: [NgIf, AsyncPipe],
  templateUrl: './cabeca.component.html',
  styleUrl: './cabeca.component.css',
})
export class CabecaComponent implements OnInit {

  constructor(private cabecaService: CabecaService, private toastr: ToastrService) { }

  cabeca = input.required<CabecaDto>();

  cabeca$!: Observable<CabecaDto>
  rotateCabeca$!: Observable<string>;
  descricaoInclinacao$!: Observable<string>;
  descricaoRotacao$!: Observable<string>;

  cabecaBackup!: CabecaDto;
  anguloInclinacao: number = 10;
  anguloRotacao: number = 0;

  ngOnInit() {
    this.cabeca$ = of(this.cabeca());
    this.transformCabeca(this.cabeca$);
  }

  obterDescricaoRotacao(cabeca: CabecaDto) {
    switch (cabeca.rotacao) {
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

  obterDescricaoInclinacao(cabeca: CabecaDto) {
    switch (cabeca.inclinacao) {
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

  transformCabeca(cabeca: Observable<CabecaDto>) {
    this.rotateCabeca$ = cabeca.pipe(
      map((cabeca) => {
        this.cabecaBackup = cabeca;

        switch (this.cabecaBackup.inclinacao) {
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

        switch (this.cabecaBackup.rotacao) {
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

        this.descricaoInclinacao$ = of(this.obterDescricaoInclinacao(cabeca));
        this.descricaoRotacao$ = of(this.obterDescricaoRotacao(cabeca));

        return `rotateX(${this.anguloInclinacao}deg) rotateY(${this.anguloRotacao}deg)`
      }));
  }

  avancarRotacaoCabeca() {
    this.transformCabeca(this.cabecaService.avancarRotacaoCabeca()
      .pipe(
        map(response => response.robo.cabeca),
        catchError((err) => {
          this.toastr.error(err.error.notificacoes[0].mensagem)
          return of(this.cabecaBackup);
        }),
      )
    )
  }

  voltarRotacaoCabeca() {
    this.transformCabeca(this.cabecaService.voltarRotacaoCabeca()
      .pipe(
        map(response => response.robo.cabeca),
        catchError((err) => {
          this.toastr.error(err.error.notificacoes[0].mensagem)
          return of(this.cabecaBackup);
        }),
      ))
  }

  avancarInclinacaoCabeca() {
    this.transformCabeca(this.cabecaService.avancarInclinacaoCabeca()
      .pipe(
        map(response => response.robo.cabeca),
        catchError((err) => {
          this.toastr.error(err.error.notificacoes[0].mensagem)
          return of(this.cabecaBackup);
        }),
      ))
  }

  voltarInclinacaoCabeca() {
    this.transformCabeca(this.cabecaService.voltarInclinacaoCabeca()
      .pipe(
        map(response => response.robo.cabeca),
        catchError((err) => {
          this.toastr.error(err.error.notificacoes[0].mensagem)
          return of(this.cabecaBackup);
        }),
      ))
  }
}
