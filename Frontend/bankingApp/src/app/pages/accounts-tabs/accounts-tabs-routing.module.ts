import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AccountsTabsPage } from './accounts-tabs.page';

const routes: Routes = [
  {
    path: '',
    component: AccountsTabsPage,
    children: [
      {
        path: 'accounts',
        loadChildren: () => import('../accounts/accounts.module').then(m => m.AccountsPageModule)
      },
      {
        path: 'cards',
        loadChildren: () => import('../cards/cards.module').then(m => m.CardsPageModule)
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
