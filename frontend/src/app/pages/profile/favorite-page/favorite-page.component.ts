import { Component, inject, OnInit } from '@angular/core';
import { KnifeBriefly } from '../../../data/interfaces/knife.interface';
import { UserService } from '../../../data/services/user.service';
import { NgxPaginationModule } from 'ngx-pagination';
import { KnifeCardComponent } from '../../../common-ui/knife-card/knife-card.component';

@Component({
  selector: 'app-favorite-page',
  imports: [KnifeCardComponent, NgxPaginationModule],
  templateUrl: './favorite-page.component.html',
  styleUrl: './favorite-page.component.scss'
})
export class FavoritePageComponent implements OnInit {
  userService = inject(UserService)
  page: number = 1;
  pageSize: number = 10;
  totalItems: number = 0;

  knifesBriefly: KnifeBriefly[] = [];

  ngOnInit() {
    this.getKnifes()
  }

  getKnifes() {
    this.userService.getFavoriteKnifesPaginated(this.page, this.pageSize).subscribe(response => {
      this.knifesBriefly = response.knifes
      this.totalItems = response.totalCount
    });
  }

  onPageChange(page: number) {
    this.page = page;
    this.getKnifes();
  }

  updatePaginationConfig() {
    this.paginationConfig = {
      id: 'pagination',
      itemsPerPage: this.pageSize,
      currentPage: this.page,
      totalItems: this.totalItems,
      previousLabel: 'Назад',
      nextLabel: 'Вперед'
    };
  }

  paginationConfig = {
    id: 'pagination',
    itemsPerPage: this.pageSize,
    currentPage: this.page,
    totalItems: this.totalItems,
    previousLabel: 'Назад',
    nextLabel: 'Вперед'
  };
}
