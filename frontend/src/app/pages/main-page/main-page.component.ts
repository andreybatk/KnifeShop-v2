import { Component } from '@angular/core';
import { CarousellComponent } from '../../common-ui/carousell/carousell.component';
import { AboutUsComponent } from "../../common-ui/about-us/about-us.component";

@Component({
  selector: 'app-main-page',
  imports: [CarousellComponent, AboutUsComponent],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent {

}
