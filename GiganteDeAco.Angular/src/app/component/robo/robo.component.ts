import { AsyncPipe, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { finalize, map, Observable, retry } from 'rxjs';
import { Lado } from '../../enums/lado';
import { RoboDto } from '../../models/roboDto';
import { RoboService } from '../../services/robo.service';
import { CabecaComponent } from "../cabeca/cabeca.component";
import { CotoveloComponent } from "../cotovelo/cotovelo.component";

@Component({
  selector: 'robo',
  imports: [NgIf, AsyncPipe, CabecaComponent, CotoveloComponent],
  templateUrl: './robo.component.html',
  styleUrl: './robo.component.css',
})
export class RoboComponent implements OnInit {

  constructor(private roboService: RoboService) { }

  robo$!: Observable<RoboDto>;
  loading: boolean = false;

  Lado = Lado;

  ngOnInit() {
    this.loading = true;
    this.robo$ = this.roboService
      .obterRobo()
      .pipe(
        map((response) => response.robo),
        retry({ count: 5, delay: 5000 }),
        finalize(() => this.loading = false)
      )
  };

  atualizarRobo() {
    this.robo$ = this.roboService
      .obterRobo()
      .pipe(map((response) => response.robo));
  }

  resetarRobo() {
    this.robo$ = this.roboService
      .resetarRobo()
      .pipe(map((response) => response.robo))
  }
}