import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { AccountService } from 'src/app/services/account/account.service';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.page.html',
  styleUrls: ['./accounts.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, RouterModule]
})
export class AccountsPage implements OnInit {

  public accounts: any[] = [];
  public fullName: string = sessionStorage.getItem('fullName') || 'User';
  constructor(private accountService: AccountService) {

   }

  ngOnInit() {
    this.accountService.UserId_Account(Number(sessionStorage.getItem('user_id'))).subscribe(account => {
      this.accounts = account;
    });
  }

}
