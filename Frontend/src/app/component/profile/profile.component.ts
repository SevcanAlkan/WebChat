import { Component, OnInit } from '@angular/core';
import { UserVM } from '@app/shared/models/user';
import { UserService } from '@app/shared/services/UserService';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '@app/shared/services/AuthenticationService';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  private unSubscribe$: Subject<void>;
  
  private user: UserVM;//It stores user data of current or which user id passed via route param.
  private id: string; //Route parameter value
  private isEditable: boolean;

  constructor(private authenticationService: AuthenticationService, private userService :UserService,
    private router: Router, private route: ActivatedRoute) {
      this.loadDefaultValues(); 
    }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id') || null;    

    if(this.id == null || this.id == ''){
      //Load current user
      this.authenticationService.currentUser
        .pipe(takeUntil(this.unSubscribe$))
        .subscribe(x => { 
          this.id = x.id
        });

      this.isEditable = true;
    }else{        
      this.isEditable = false;
    }    

    this.userService.GetById(this.id)
      .pipe(takeUntil(this.unSubscribe$))
      .subscribe(x => {
        if(x && x.rec){
        this.user = x.rec;
        }
      });

    window.onunload = () => this.ngOnDestroy();
  }

  ngOnDestroy() : any {
    this.unSubscribe$.next();
    this.unSubscribe$.complete();

    this.loadDefaultValues();
  }

  private loadDefaultValues() : void {
    this.user = null;
    this.isEditable = false;

    this.unSubscribe$ = new Subject<void>();   
  }

  edit(){
    this.router.navigate(['register', this.user.id]);
  }

  returnBack(){
    this.router.navigate(['/']);
  }
}
