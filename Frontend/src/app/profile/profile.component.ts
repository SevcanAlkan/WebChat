import {Component, OnInit} from '@angular/core';
import {User} from '@app/Models/user';
import {UserService} from '@services/UserService';
import {ActivatedRoute, Router} from '@angular/router';
import {AuthenticationService} from '@services/AuthenticationService';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  private user: User = new User();//It stores user data of current or which user id passed via route param.
  private id: string; //Route parameter value
  private isEditable: boolean = false;

  constructor(private authenticationService: AuthenticationService, private userService: UserService,
              private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id') || null;

    if (this.id == null || this.id == '') {
      //Load current user
      this.authenticationService.currentUser.subscribe(x => {
        this.id = x.id
      });

      this.isEditable = true;
    } else {
      this.isEditable = false;
    }

    this.userService.GetById(this.id).subscribe(x => {
      if (x && x.rec) {
        this.user = x.rec;
      }
    });
  }

  ngDestroy() {
  }

  edit() {
    this.router.navigate(['register', this.user.id]);
  }

  returnBack() {
    this.router.navigate(['/']);
  }
}
