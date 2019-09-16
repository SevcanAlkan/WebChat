import { Component, OnInit } from '@angular/core';
import { UserRegisterVM } from '@models/User';
import { Router } from '@angular/router';
import { AuthenticationService } from '@services/AuthenticationService';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  private user: UserRegisterVM = new UserRegisterVM();
  private comparePassword: string = '';

  // Validation
  private userNameIsntValid: boolean = false;
  private displayNameIsntValid: boolean = false;
  private passwordIsntValid: boolean = false;
  private comparePasswordIsntValid: boolean = false;
  private aboutIsntValid: boolean = false;

  constructor(private router: Router, private authenticationService: AuthenticationService) { 

  }

  ngOnInit() {

  }

  save(){
    if(String(this.user.userName).length < 4 || String(this.user.userName).length > 15){
      this.userNameIsntValid = true;
      return;
    }else{
      this.userNameIsntValid = false;
    }

    if(String(this.user.displayName).length < 4 || String(this.user.displayName).length > 20){
      this.displayNameIsntValid = true;
      return;
    }else{
      this.displayNameIsntValid = false;
    }

    var passwordIsValid = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])");
    if(String(this.user.passwordHash).length < 6 
    || String(this.user.passwordHash).length > 50
    || !passwordIsValid.test(this.user.passwordHash)){
      this.passwordIsntValid = true;
      return;
    }else{
      this.passwordIsntValid = false;
    }

    if(this.user.passwordHash != this.comparePassword){
      this.comparePasswordIsntValid = true;
      return;
    }else{
      this.comparePasswordIsntValid = false;
    }

    if(String(this.user.displayName).length < 4 || String(this.user.displayName).length > 20){
      this.displayNameIsntValid = true;
      return;
    }else{
      this.displayNameIsntValid = false;
    }

    if(String(this.user.about).length > 250){
      this.aboutIsntValid = true;
      return;
    }else{
      this.aboutIsntValid = false;
    }

    this.user.status = 4;
    console.log(this.user);
    this.authenticationService.register(this.user).subscribe( () => {
      this.returnBack();
    });
  }

  returnBack(){
    this.router.navigate(['login']);
  }

}
