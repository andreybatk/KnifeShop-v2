import { Component, inject, OnInit } from '@angular/core';
import { Knife } from '../../data/interfaces/knife.interface';
import { KnifeService } from '../../data/services/knife.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ImgUrlPipe } from '../../helpers/pipes/img-url.pipe';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-knife-page',
  imports: [CommonModule, ImgUrlPipe],
  templateUrl: './knife-page.component.html',
  styleUrl: './knife-page.component.scss'
})
export class KnifePageComponent implements OnInit {
  route = inject(ActivatedRoute)
  authService = inject(AuthService);
  knifeService = inject(KnifeService)
  router = inject(Router)

  knife:Knife | null = null;
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
    this.router.navigate([`edit-knife/${this.id}`])
  }

  get isAdmin(): boolean {
    return this.authService.hasRole('Admin');
  }
}
