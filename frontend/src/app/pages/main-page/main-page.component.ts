import { Component } from '@angular/core';
import { CarousellComponent } from '../../common-ui/carousell/carousell.component';
import { AboutUsComponent } from "../../common-ui/about-us/about-us.component";
import { CatalogToActionComponent } from "../../common-ui/catalog-to-action/catalog-to-action.component";
import { FooterComponent } from "../../common-ui/footer/footer.component";
import { CategoriesComponent } from "../../common-ui/categories/categories.component";

@Component({
  selector: 'app-main-page',
  imports: [CarousellComponent, AboutUsComponent, CatalogToActionComponent, FooterComponent, CategoriesComponent],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent {

}
