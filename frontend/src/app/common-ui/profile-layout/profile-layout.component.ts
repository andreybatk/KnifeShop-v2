import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ProfileSidebarComponent } from '../profile-sidebar/profile-sidebar.component';

@Component({
  selector: 'app-profile-layout',
  imports: [RouterOutlet, ProfileSidebarComponent],
  templateUrl: './profile-layout.component.html',
  styleUrl: './profile-layout.component.scss'
})
export class ProfileLayoutComponent {

}
