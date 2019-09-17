import { Component, OnInit } from '@angular/core';
import { UserRegisterVM, User, UserUpdateVM } from '@models/User';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '@services/AuthenticationService';
import { UserService } from '@app/services/UserService';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  private user: UserRegisterVM = new UserRegisterVM();
  private userRec: UserUpdateVM = new UserUpdateVM();
  private comparePassword: string = '';
  private id:string;
  private alreadyRegistredUser: boolean = false;

  // Validation
  private userNameIsntValid: boolean = false;
  private userNameAlreadyTaken: boolean = false;
  private displayNameIsntValid: boolean = false;
  private passwordIsntValid: boolean = false;
  private comparePasswordIsntValid: boolean = false;
  private OldPasswordIsntValid: boolean = false;
  private aboutIsntValid: boolean = false;

  constructor(private router: Router, private authenticationService: AuthenticationService,
    private userService: UserService, private route: ActivatedRoute) { 

  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id') || null; 

    if(this.id != null && this.id != ''){
      this.userService.GetById(this.id).subscribe(x => {
        if(x && x.rec){
         this.userRec = x.rec;
            
          this.user.about = this.userRec.about;
          this.user.displayName = this.userRec.displayName;
          this.user.isAdmin = this.userRec.isAdmin;
          this.user.status = this.userRec.status;
          // this.user.passwordHash = this.userRec.passwordHash;
          this.user.userName = this.userRec.userName;

          console.log(this.userRec);
        }
      });      

      this.alreadyRegistredUser = true;
    }
  }

  save(){    
    if(String(this.user.userName).length < 4 || String(this.user.userName).length > 15){
      this.userNameIsntValid = true;
      return;
    }else{     
      this.userNameIsntValid = false;  
    }

    this.userService.isUserNameExist(this.user.userName).subscribe(x=> this.userNameAlreadyTaken = x);      
    if(this.userNameAlreadyTaken){
      return;
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

    if(this.alreadyRegistredUser && (String(this.userRec.oldPassword).length < 6 
    || String(this.userRec.oldPassword).length > 50)){
      this.OldPasswordIsntValid = true;
      return;
    }else{
      this.OldPasswordIsntValid = false;
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
    
    if(!this.alreadyRegistredUser){
      this.authenticationService.register(this.user).subscribe( () => {
        this.returnBack();
      });
    }else{
      
      this.userRec.about = this.user.about;
      this.userRec.displayName = this.user.displayName;
      this.userRec.isAdmin = this.user.isAdmin;
      this.userRec.passwordHash = this.user.passwordHash;            
      this.userRec.userName = this.user.userName;

      this.userService.Update(this.userRec).subscribe(() => {
        this.returnBack();
      })
    }
  }

  returnBack(){
    if(this.alreadyRegistredUser){
      this.authenticationService.logout();
    }

    this.router.navigate(['login']);
  }

}
