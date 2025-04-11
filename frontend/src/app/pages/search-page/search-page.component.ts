import { Component, inject } from '@angular/core';
import { KnifeService } from '../../data/services/knife.service';
import { KnifeBriefly, } from '../../data/interfaces/knife-briefly.interface';
import { KnifeCardComponent } from '../../common-ui/knife-card/knife-card.component';

@Component({
  selector: 'app-search-page',
  imports: [KnifeCardComponent],
  templateUrl: './search-page.component.html',
  styleUrl: './search-page.component.scss',
  providers: [KnifeService]
})

export class SearchPageComponent {
  knifeService = inject(KnifeService)
  knifesBriefly: KnifeBriefly[] = []
  
  constructor() {
    this.knifeService.getKnifesPaginated()
      .subscribe(val => {
        this.knifesBriefly = val
    })
  }
}
