import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'home',
    loadComponent: () => import('./pages/home/home.page').then(m => m.HomePage)
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login.page').then(m => m.LoginPage)
  },
  {
    path: 'register',
    loadComponent: () => import('./pages/register/register.page').then(m => m.RegisterPage)
  },
  {
    path: 'transactions',
    loadComponent: () => import('./pages/transactions/transactions.page').then(m => m.TransactionsPage)
  },
  {
    path: 'transfer',
    loadComponent: () => import('./pages/transfer/transfer.page').then(m => m.TransferPage)
  },
  {
    path: 'transfer-success',
    loadComponent: () => import('./pages/transfer-success/transfer-success.page').then(m => m.TransferSuccessPage)
  },
  {
    path: 'accounts-tabs',
    loadComponent: () => import('./pages/accounts-tabs/accounts-tabs.page').then(m => m.AccountsTabsPage),
    children: [
      {
        path: 'accounts',
        loadComponent: () => import('./pages/accounts/accounts.page').then(m => m.AccountsPage)
      },
      {
        path: 'cards',
        loadComponent: () => import('./pages/cards/cards.page').then(m => m.CardsPage)
      },
      {
        path: '',
        redirectTo: 'accounts',
        pathMatch: 'full'
      }
    ]
  }
];
