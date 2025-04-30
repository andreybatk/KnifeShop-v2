import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RoleService } from '../../../data/services/role.service';
import { RoleDto } from '../../../data/interfaces/role.interface';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-edit-roles-page',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './edit-roles-page.component.html',
  styleUrl: './edit-roles-page.component.scss'
})
export class EditRolesPageComponent implements OnInit {
  roleService = inject(RoleService)
  allRoles: string[] | null = null
  currentRoles: string[] | null = null
  currentEmail: string | null = null
  errorMessage: string = ''

  form = new FormGroup({
    email: new FormControl<string | null>(null, [Validators.required, Validators.email]),
  });

  ngOnInit() {
    this.roleService.getRoles().subscribe(r => this.allRoles = r)
  }

  onSubmit() {
    if (this.form.valid) {
      const email = this.form.value.email ?? null;
      this.roleService.getUserRoles(email)
      .pipe(
        catchError(error => {
          this.errorMessage = 'Ошибка. Пользователь не найден.';
          return throwError(() => error);
        })
      )
      .subscribe({
        next: (r) => {
          this.errorMessage = ''
          this.currentRoles = r
          this.currentEmail = email
        },
        error: () => {
        }
      });
    }
  }
  
  onRoleChange(role: string, event: any) {
    const email = this.form.value.email;
    if (!email || !this.currentRoles) return;
  
    const payload: RoleDto = {
      email,
      role: role,
    };
  
    if (event.target.checked) {
      this.roleService.addRoles(payload).subscribe(() => {
        if (!this.currentRoles!.includes(role)) {
          this.currentRoles!.push(role);
        }
      });
    } else {
      this.roleService.deleteRoles(payload).subscribe(() => {
        this.currentRoles = this.currentRoles!.filter(r => r !== role) ?? null;
      });
    }
    alert('Роль пользователя успешно изменена.')
  }
}
