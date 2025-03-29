import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RoboComponent } from "./robo/robo.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RoboComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'GiganteDeAco.Angular';
}
