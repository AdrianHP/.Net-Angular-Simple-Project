import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../account/services/auth.service';
import { map, take } from 'rxjs';
import { StorageService } from '../account/services/storage.service';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const userData = inject(StorageService);
  const user = userData.getUser();
  if (user) {
    return true;
    return router.navigate(['forbidden']);
  }
  return router.navigate(['account/login']);
};
