import { Component, inject, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ImgUrlPipe } from '../../helpers/pipes/img-url.pipe';
import { KnifeBriefly } from '../../data/interfaces/knife.interface';
import { Router } from '@angular/router';
import { UserService } from '../../data/services/user.service';
import { AuthService } from '../../auth/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-knife-card',
  imports: [CommonModule, ImgUrlPipe],
  templateUrl: './knife-card.component.html',
  styleUrl: './knife-card.component.scss'
})
export class KnifeCardComponent {
  @Input() knife!: KnifeBriefly;
  router = inject(Router)

  userService = inject(UserService);
  authService = inject(AuthService);

  toggleFavorite(event: MouseEvent) {
    event.stopPropagation();

    if(!this.authService.isAuth)
    {
      this.router.navigate(['login'])
      return
    }

    if (this.knife.isFavorite) {
      this.userService.removeFavoriteKnife(this.knife.id).subscribe(() => {
        this.knife.isFavorite = false;
      });
    } else {
      this.userService.addFavoriteKnife(this.knife.id).subscribe(() => {
        this.knife.isFavorite = true;
      });
    }
  }

  onClickCard() {
    this.router.navigate([`knife/${this.knife.id}`])
  }
}
