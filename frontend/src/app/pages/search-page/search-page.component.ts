import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { KnifeService } from '../../data/services/knife.service';
import { KnifePaginated } from '../../data/interfaces/knife-paginated.interface';
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
  knifesPaginated: KnifePaginated[] = []
  
  constructor() {
    this.knifeService.getKnifesPaginated()
      .subscribe(val => {
        this.knifesPaginated = val
    })
  }
}
