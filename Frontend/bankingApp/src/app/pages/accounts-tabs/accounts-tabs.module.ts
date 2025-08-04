import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';

import { AccountsTabsPageRoutingModule } from './accounts-tabs-routing.module';
// import { AccountsTabsPage } from './accounts-tabs.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    AccountsTabsPageRoutingModule
  ],
  declarations: [
    // AccountsTabsPage
  ]
})
export class AccountsTabsPageModule {}