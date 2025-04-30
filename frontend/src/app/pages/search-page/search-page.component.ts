import { Component, inject, OnInit } from '@angular/core';
import { KnifeService } from '../../data/services/knife.service';
import { KnifeCardComponent } from '../../common-ui/knife-card/knife-card.component';
import { GetKnifesPaginationDto, KnifeBriefly } from '../../data/interfaces/knife.interface';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

@Component({
  selector: 'app-search-page',
  imports: [KnifeCardComponent, FormsModule, NgxPaginationModule],
  templateUrl: './search-page.component.html',
  styleUrl: './search-page.component.scss',
  providers: [KnifeService]
})

export class SearchPageComponent implements OnInit {
  knifeService = inject(KnifeService)
  
  search = '';
  sortItem = 'title';
  sortOrder: 'asc' | 'desc' = 'asc';
  page: number = 1;
  pageSize: number = 12;
  totalItems: number = 0;
  knifesBriefly: KnifeBriefly[] = [];

  ngOnInit() {
    this.getKnifes()
  }

  getKnifes() {
    const request: GetKnifesPaginationDto = {
      search: this.search,
      sortItem: this.sortItem,
      sortOrder: this.sortOrder,
      page: this.page,
      pageSize: this.pageSize,
      categoryIds: null //TODO
    };

    this.knifeService.getKnifesPaginated(request).subscribe(response => {
      this.knifesBriefly = response.knifes
      this.totalItems = response.totalCount
      this.updatePaginationConfig()
    });
  }

  onSearch() {
    this.page = 1;
    this.getKnifes()
  }

  onSortChange() {
    this.page = 1;
    this.getKnifes()
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
