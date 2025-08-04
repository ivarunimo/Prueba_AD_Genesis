export interface DebitCards {
  id: number;
  number: string;
  active: boolean;
  id_account: number;
  id_user: number;
  type: string;
  expDate: string; // Optional property for card expiration date
  name_card: string;
  balance?: number; // Optional property to hold the balance
}