import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user/user.service';
import { Router } from '@angular/router';
import { ToastController } from '@ionic/angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, RouterModule, ReactiveFormsModule]
})
export class LoginPage implements OnInit {

  loginForm: FormGroup;

  constructor(
    private userService: UserService,
    private router: Router,
    private fb: FormBuilder,
    private toastController: ToastController
  ) { 
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit() {
  }



  onSubmitLogin(){
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;
      this.userService.login(username, password).subscribe(
        response => {
          sessionStorage.setItem('token', response.token);
          sessionStorage.setItem('user_id', response.user_id);
          sessionStorage.setItem('fullName', response.fullName);
          this.router.navigate(['/home']);
        },
        async error => {
          const toast = await this.toastController.create({
              message: 'Credenciales incorrectas, tu usuario o contraseña son incorrectos',
              duration: 3000,
              color: 'danger', // success, danger, warning, etc.
              position: 'middle', // 'top', 'middle', 'bottom'
              icon: 'close-circle-outline',
            });
            toast.present();
          // Handle login error, e.g., show an alert
        }
      );
    } else {
      alert('Por favor llenar campos correctamente');
    }
  }

}
