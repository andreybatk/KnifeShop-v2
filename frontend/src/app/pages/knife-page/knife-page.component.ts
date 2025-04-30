import { Component, inject, OnInit } from '@angular/core';
import { Knife } from '../../data/interfaces/knife.interface';
import { KnifeService } from '../../data/services/knife.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ImgUrlPipe } from '../../helpers/pipes/img-url.pipe';
import { AuthService } from '../../auth/auth.service';
import { FavoriteService } from '../../data/services/favorite.service';

@Component({
  selector: 'app-knife-page',
  imports: [CommonModule, ImgUrlPipe],
  templateUrl: './knife-page.component.html',
  styleUrl: './knife-page.component.scss'
})
export class KnifePageComponent implements OnInit {
  route = inject(ActivatedRoute)
  router = inject(Router)
  authService = inject(AuthService);
  knifeService = inject(KnifeService)
  favoriteService = inject(FavoriteService)

  knife:Knife | null = null;
  baseUrl = 'http://localhost:4200/knife'
  baseApiUrl = 'http://localhost:5000';
  id:number | null = null;

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (this.id) {
      this.knifeService.getKnife(this.id).subscribe((knife) => this.knife = knife);
    }
  }

  selectedImage: string | null = null;

  openImage(url: string | null) {
    if(url)
    {
      this.selectedImage = `${this.baseApiUrl}${url}`;
    }
  }

  closeImage() {
    this.selectedImage = null;
  }

  onClickEdit() {
    this.router.navigate([`admin/edit-knife/${this.id}`])
  }

  onClickSendMessage() {
    const message = `Здравствуйте. Меня заинтересовал товар ${this.knife?.title} (${this.baseUrl}/${this.knife?.id}).`;
    const username = 'therealbushcraft';
    const url = `https://t.me/${username}?text=${encodeURIComponent(message)}`;
    window.open(url, '_blank');
  }

  toggleFavorite(event: MouseEvent) {
    event.stopPropagation();
    
    if (!this.knife?.id) return;
  
    this.favoriteService.toggleFavorite(this.knife.id, this.knife.isFavorite)
      .subscribe((newValue: boolean) => {
        this.knife!.isFavorite = newValue;
      });
  }
}
