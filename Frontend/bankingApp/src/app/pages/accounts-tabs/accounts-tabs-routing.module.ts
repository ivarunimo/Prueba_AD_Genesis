import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AccountsTabsPage } from './accounts-tabs.page';
import { authGuard } from 'src/app/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: AccountsTabsPage,
    children: [
      {
        path: 'accounts',
        loadChildren: () => import('../accounts/accounts.module').then(m => m.AccountsPageModule),
        canActivate: [authGuard]
      },
      {
        path: 'cards',
        loadChildren: () => import('../cards/cards.module').then(m => m.CardsPageModule),
        canActivate: [authGuard]
      },
      {
        path: '',
        redirectTo: 'accounts',
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AccountsTabsPageRoutingModule {}
