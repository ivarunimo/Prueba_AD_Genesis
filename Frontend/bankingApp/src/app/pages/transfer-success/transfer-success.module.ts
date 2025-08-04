import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';

import { TransferSuccessPageRoutingModule } from './transfer-success-routing.module';
import { TransferSuccessPage } from './transfer-success.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    TransferSuccessPageRoutingModule
  ],
  declarations: [TransferSuccessPage]
})
export class TransferSuccessPageModule {}