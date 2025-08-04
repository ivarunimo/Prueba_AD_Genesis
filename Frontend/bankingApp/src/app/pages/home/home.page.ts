import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DebitCardService } from 'src/app/services/debitCard/debit-card.service';
import { DebitCards } from 'src/app/models/DebitCards';
import { AccountService } from 'src/app/services/account/account.service';
import { Movements } from 'src/app/models/Movements';
import { MovementService } from 'src/app/services/movement/movement.service';
import { ToastController } from '@ionic/angular';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, RouterModule],
})
export class HomePage implements OnInit {

  public debitCard: DebitCards = {
    id: 0,
    number: '',
    active: false,
    id_account: 0,
    id_user: 0,
    type: '',
    expDate: '',
    name_card: '',
    balance: 0
  };
  public fullName: string = sessionStorage.getItem('fullName') || 'User';
  public movimientos: Movements[] = [];

  public balance: number = 0;
  constructor(
    private debitCardService: DebitCardService,
    private accountService: AccountService,
    private movementsService: MovementService,
    private toastController: ToastController
  ) { }

  ngOnInit() {
    this.debitCardService.getDebitCards(Number(sessionStorage.getItem('user_id'))).subscribe(cards => {
      console.log(cards);
      if (cards && cards.length > 0) {
        this.debitCard = {
          id: cards[0].id,
          number: cards[0].number,
          active: cards[0].active,
          id_account: cards[0].id_account,
          id_user: cards[0].id_user,
          type: cards[0].type,
          expDate: cards[0].expDate,
          name_card: cards[0].name_card,
        };
        this.accountService.getAccountById(cards[0].id_account).subscribe(account => {
          this.balance = account.balance;

          this.movementsService.getMovements(account.id).subscribe(movements => {
            this.movimientos = movements;
            console.log('Movements:', this.movimientos);
          });
        });


      }
    });


  }

  async navigateToBalance() {
    const toast = await this.toastController.create({
      message: 'Datos guardados correctamente',
      duration: 3000,
      color: 'success', // success, danger, warning, etc.
      position: 'middle', // 'top', 'middle', 'bottom'
      icon: 'checkmark-circle-outline',
    });
    toast.present();
  }

}
