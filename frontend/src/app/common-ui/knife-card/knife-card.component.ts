import { Component, inject, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ImgUrlPipe } from '../../helpers/pipes/img-url.pipe';
import { KnifeBriefly } from '../../data/interfaces/knife.interface';
import { Router } from '@angular/router';
import { UserService } from '../../data/services/user.service';
import { AuthService } from '../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { FavoriteService } from '../../data/services/favorite.service';

@Component({
  selector: 'app-knife-card',
  imports: [CommonModule, ImgUrlPipe],
  templateUrl: './knife-card.component.html',
  styleUrl: './knife-card.component.scss'
})
export class KnifeCardComponent {
  @Input() knife!: KnifeBriefly;
  router = inject(Router)
  favoriteService = inject(FavoriteService)
  userService = inject(UserService);
  authService = inject(AuthService);

  toggleFavorite(event: MouseEvent) {
    event.stopPropagation();
    
    if (!this.knife?.id) return;
  
    this.favoriteService.toggleFavorite(this.knife.id, this.knife.isFavorite)
      .subscribe((newValue: boolean) => {
        this.knife.isFavorite = newValue;
      });
  }

  onClickCard() {
    this.router.navigate([`knife/${this.knife.id}`])
  }
}
