import { Component, OnInit } from '@angular/core';
import { RoboService } from '../services/robo.service';
import { AsyncPipe, NgIf } from '@angular/common';
import { RoboDto } from '../models/roboDto';
import { map, Observable } from 'rxjs';
import { Lado } from '../enums/lado';
import { PulsoComponent } from "../pulso/pulso.component";
import { CabecaComponent } from "../cabeca/cabeca.component";
import { CotoveloComponent } from "../cotovelo/cotovelo.component";

@Component({
  selector: 'robo',
  imports: [NgIf, AsyncPipe, PulsoComponent, CabecaComponent, CotoveloComponent],
  templateUrl: './robo.component.html',
  styleUrl: './robo.component.css',
})
export class RoboComponent implements OnInit{
  
  constructor(private roboService : RoboService){}
  
  robo$!: Observable<RoboDto>;
  roboErro$: Observable<Error> | undefined;
  Lado = Lado;

  ngOnInit(){
    this.robo$ = this.roboService
      .obterRobo()
      .pipe(map((response) => response.robo));
  };

  atualizarRobo(){
    this.robo$ = this.roboService
      .obterRobo()
      .pipe(map((response) => response.robo));
  }

  resetarRobo(){
    this.robo$ = this.roboService
      .resetarRobo()
      .pipe(map((response) => response.robo))
  }
}