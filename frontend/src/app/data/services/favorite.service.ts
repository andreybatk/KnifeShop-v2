import { inject, Injectable } from "@angular/core";
import { AuthService } from "../../auth/auth.service";
import { Router } from "@angular/router";
import { UserService } from "./user.service";
import { map, Observable, of, tap } from "rxjs";

@Injectable({ providedIn: 'root' })

export class FavoriteService {
  authService = inject(AuthService)
  router = inject(Router)
  userService = inject(UserService)


  toggleFavorite(knifeId: number, isFavorite:boolean): Observable<boolean> {
    if (!this.authService.isAuth) {
      this.router.navigate(['login']);
      return of(false); 
    }

    const obs = isFavorite
      ? this.userService.removeFavoriteKnife(knifeId)
      : this.userService.addFavoriteKnife(knifeId);

    return obs.pipe(
      tap(() => isFavorite = !isFavorite),
      map(() => isFavorite)
    );
  }
}
