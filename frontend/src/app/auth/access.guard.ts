import { inject } from "@angular/core"
import { AuthService } from "./auth.service"
import { Router } from "@angular/router"

export const canActivateAuth = () => {
  const isLoggedIn = inject(AuthService).isAuth

  if(isLoggedIn) {
      return true
  }

  return inject(Router).createUrlTree(['/login'])
}

export const canActivateGuest = () => {
  const isLoggedIn = inject(AuthService).isAuth;

  if (!isLoggedIn) {
    return true;
  }

  return inject(Router).createUrlTree(['/search']);
};

export const canActivateRole = (requiredRoles: string[]) => {
  return () => {
    const auth = inject(AuthService);
    const router = inject(Router);

    if (!auth.isAuth) {
      return router.createUrlTree(['/login']);
    }

    const userRoles = auth.userRoles;

    const hasRequiredRole = requiredRoles.some(role => userRoles.includes(role));

    if (hasRequiredRole) {
      return true;
    }

    return router.createUrlTree(['/access-denied']);
  };
};