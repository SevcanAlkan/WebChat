import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '@app/shared/services/AuthenticationService';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  private unSubscribe$: Subject<void>; //Not used on this component
  
  loginForm: FormGroup;
  loading: boolean;
  submitted: boolean;
  returnUrl: string;
  error: string;

  constructor( private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) {
      if (this.authenticationService.CurrentUserValue) { 
        this.router.navigate(['/']);
      }

      this.loadDefaultValues(); 
    }

  ngOnInit() {
    this.loginForm = this.createFrom();

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

    window.onunload = () => this.ngOnDestroy();
  }

  ngOnDestroy() : any {
    this.unSubscribe$.next();
    this.unSubscribe$.complete();

    this.loadDefaultValues();
  }

  private loadDefaultValues() : void {
    this.loading = false;
    this.submitted = false;
    this.error = '';
    this.loginForm = this.createFrom();

    this.unSubscribe$ = new Subject<void>();   
  }

  private createFrom() : FormGroup {
    return this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.loginForm.invalid) {
        return;
    }

    this.loading = true;
    this.authenticationService.login(this.f.username.value, this.f.password.value)
        .pipe(first())
        .subscribe(
            data => {
                this.router.navigate([this.returnUrl]);
            },
            error => {
                this.error = error;
                this.loading = false;
            });
  }

  register(){  
      this.router.navigate(['register']);    
  }
}
