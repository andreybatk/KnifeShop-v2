import { Component, inject } from '@angular/core';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-profile-page',
  imports: [],
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.scss'
})
export class ProfilePageComponent {
  authService = inject(AuthService)
  
}
