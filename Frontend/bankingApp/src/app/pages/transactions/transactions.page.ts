import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { MovementService } from 'src/app/services/movement/movement.service';
import { Movements } from 'src/app/models/Movements';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.page.html',
  styleUrls: ['./transactions.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, RouterModule]
})
export class TransactionsPage implements OnInit {

  public movimientos: Movements[] = [];
  constructor(
    private movementService: MovementService
  ) { }

  ngOnInit() {
    const userId = Number(sessionStorage.getItem('user_id'));
    this.movementService.getUserMovements(userId).subscribe(movements => {
      this.movimientos = movements;
      console.log('Movements:', this.movimientos);
    });
  }


}
