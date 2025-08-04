import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { DebitCardService } from 'src/app/services/debitCard/debit-card.service';
import { DebitCards } from 'src/app/models/DebitCards';
import { AccountService } from 'src/app/services/account/account.service';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.page.html',
  styleUrls: ['./cards.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, RouterModule]
})
export class CardsPage implements OnInit {

  public debitCards: DebitCards[] = [];
  public fullName: string = sessionStorage.getItem('fullName') || 'User';

  constructor(
    private debitCardService: DebitCardService,
    private accountService: AccountService
  ) { }

  ngOnInit() {
    this.debitCardService.getDebitCards(Number(sessionStorage.getItem('user_id'))).subscribe(cards => {
      this.debitCards = cards;
      cards.forEach(card => {
        this.accountService.getAccountById(card.id_account).subscribe(account => {
          this.debitCards.find(c => c.id === card.id)!.balance = account.balance;
        });
      });


      console.log('Debit Cards:', this.debitCards);
    });
  }

}
