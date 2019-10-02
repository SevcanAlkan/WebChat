import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
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
  private alreadyRegistredUser: boolean;
  private submitted = false;

  private passwordValid: RegExp;
  private registerForm: FormGroup;  

  constructor(private router: Router, private authenticationService: AuthenticationService,
    private userService: UserService, private route: ActivatedRoute, private fb: FormBuilder) { 
      this.alreadyRegistredUser = false;
      this.passwordValid = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])");
      this.registerForm = this.createFrom();
  }
  
  ngOnInit() {   
    this.id = this.route.snapshot.paramMap.get('id') || null; 

    if(this.id != null && this.id != ''){
      this.userService.GetById(this.id).subscribe(x => {
        if(x && x.rec){
          this.userRec = x.rec;            

          this.registerForm.patchValue({
             userName: this.userRec.userName,
             displayName: this.userRec.displayName,
             passwordGroup: {
             password: "",
             confirmPassword: "",
             oldPassword: "",
             },
             about: this.userRec.about,
             isAdmin: this.userRec.isAdmin
          });
        }
      });      

      this.alreadyRegistredUser = true;
    } 
  }

  get f() { return this.registerForm.controls; }
  get userName() { return this.registerForm.get('userName'); }
  get displayName() { return this.registerForm.get('displayName'); }
  get password() { return this.registerForm['controls'].passwordGroup['controls'].password; }
  get confirmPassword() { return this.registerForm['controls'].passwordGroup['controls'].confirmPassword; }
  get oldPassword() { return this.registerForm['controls'].passwordGroup['controls'].oldPassword; }
  get about() { return this.registerForm.get('about'); }
  get isAdmin() { return this.registerForm.get('isAdmin'); }
  createFrom() : FormGroup {
    return this.fb.group({
      userName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(15),  this.checkUserNameIsAvailable.bind(this)]],
      displayName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
      passwordGroup: this.fb.group({    
        password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(50)]],
        confirmPassword: [''],
        oldPassword: [''], 
      }, {validator: [this.pwdMatchValidator,  this.checkPassword.bind(this), this.checkOldPassword.bind(this)]}),
      about: ['', Validators.maxLength(250)],
      isAdmin: ['']
    });   
  }

  pwdMatchValidator(frm: FormGroup) {
    let pass = frm.get('password');
    let confirmPass = frm.get('confirmPassword');
  
    return pass.value === confirmPass.value ? null : confirmPass.setErrors({ notSame: true }); 
  }
  checkUserNameIsAvailable(control: AbstractControl){
    let userName = control;

    if(this.alreadyRegistredUser == null){
      return null;
    }else{
      if(userName.value.length > 4
        && ((this.alreadyRegistredUser && this.userRec && this.userRec.userName != userName.value) 
        || !this.alreadyRegistredUser)){
        this.userService.isUserNameExist(userName.value).subscribe(x =>  {       
          return x ? userName.setErrors({ isTaken: true }) : null;
        });      
      }else{
        return null;
      } 
    } 
  }
  checkPassword(frm: FormGroup){
    let pass =  frm.get('password');
    let passValid = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])");

    if(pass.value && pass.value.length > 8 && !passValid.test(pass.value)){
      return pass.setErrors({ passwordIsntStrong: true });
    }else{
      return null;
    }
  }
  checkOldPassword(frm: FormGroup){
    let pass =  frm.get('oldPassword');

    if(this.alreadyRegistredUser && (!pass.value || pass.value.length < 8 )){
      return pass.setErrors({ mustEnterOldPass: true });
    // }else if(this.alreadyRegistredUser && pass.value != this.userRec.passwordHash){
    //   return pass.setErrors({ oldPassNotCorrect: true });
    }else{
      return null;
    }
  }

  save(){    
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }

    if(this.alreadyRegistredUser == null || !this.alreadyRegistredUser){
     
      this.user.about = this.about.value;
      this.user.displayName = this.displayName.value;
      this.user.isAdmin = this.isAdmin.value ? true : false;
      this.user.passwordHash = this.password.value;            
      this.user.userName = this.userName.value;
      this.user.status = 4;

      this.authenticationService.register(this.user).subscribe( () => {
        this.returnBack();
      });
    }else{
      
      this.userRec.about = this.about.value;
      this.userRec.displayName = this.displayName.value;
      this.userRec.isAdmin = this.isAdmin.value ? true : false;
      this.userRec.passwordHash = this.password.value;            
      this.userRec.userName = this.userName.value;
      this.userRec.oldPassword = this.oldPassword.value;
      
      this.userService.Update(this.userRec).subscribe(() => {
        this.returnBack();
      })
    }

    this.submitted = false;
  }

  returnBack(){
    if(this.alreadyRegistredUser){
      this.authenticationService.logout();
    }

    this.router.navigate(['login']);
  }

  cancel(){
    if(this.alreadyRegistredUser){
      this.router.navigate(['/']);
    }else{
      this.router.navigate(['login']);
    }
  }

}
