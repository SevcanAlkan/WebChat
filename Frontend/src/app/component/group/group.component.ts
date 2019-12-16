import { Component, OnInit, HostListener } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupService } from '@app/services/GroupService';
import { GroupVM, GroupUpdateVM, GroupAddVM } from '@app/models/Group';
import { APIResultVM } from '@app/common/APIResultVM';
import { UserService } from '@services/UserService';
import { UserListVM, UserVM } from '@app/models/User';
import { AuthenticationService } from '@services/AuthenticationService';
import { Observable, Subscription, Subject, of } from 'rxjs';
import { takeUntil, map, mapTo } from 'rxjs/operators';
import { DataTrackingService } from '@app/services/DataTrackingService';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {
  private unSubscribe$: Subject<void>;

  private group: GroupVM;
  private oldRec: GroupVM;
  private isNew: boolean;
  private id: string;
  private isDeleteVisible: boolean;

  private nameIsntValid: boolean;
  private descIsntValid: boolean;

  private userSearchKey: string;
  private userList: UserListVM[];  
  private filteredUserList: UserListVM[];  
  private selectedUsers: UserListVM[];  

  private CurrentUser : UserVM;
  
  constructor(private groupService : GroupService, private userService : UserService,
     private router: Router, private route: ActivatedRoute,
     private authenticationService: AuthenticationService,
     private dataTrackingService: DataTrackingService) { 
      this.loadDefaultValues(); 
   }

  ngOnInit() {
    this.CurrentUser = this.authenticationService.CurrentUserValue; 

    this.id = this.route.snapshot.paramMap.get('id') || null;        

    this.userService.getUserList()
    .pipe(takeUntil(this.unSubscribe$))
    .subscribe(x => {
      x.forEach((item: UserListVM) => {    
        this.userList.push(item);                
      });  
    });    

    if(this.id){      
      this.groupService.GetById(this.id)
      .pipe(takeUntil(this.unSubscribe$))
      .subscribe((item: APIResultVM) => {     
        if(item.rec) {
          this.group = item.rec;
          this.oldRec = item.rec;
          this.isNew = false;   

          this.groupService.GetUsers(this.id)
          .pipe(takeUntil(this.unSubscribe$))
          .subscribe(x => {
            if(x && x.length>0){
              this.group.users = x;  
              this.selectedUsers = this.userList.filter(a => this.group.users.some(x => x == a.id));                           
            }
          });
                      
          if(!this.group.isMain){
            this.isDeleteVisible = true
          }           
        }
      });
    }else{
      this.isNew = true;
      this.isDeleteVisible = false;
    }  

    this.dataTrackingService.OpenConnection();

    window.onunload = () => this.ngOnDestroy();
  }

  ngOnDestroy() : any {
    this.unSubscribe$.next();
    this.unSubscribe$.complete();

    this.dataTrackingService.CloseConnection();

    this.loadDefaultValues();
  }

  private loadDefaultValues() : void {
    this.group = new GroupVM();
    this.isNew = false;
    this.isDeleteVisible = false;
    this.nameIsntValid = false;
    this.descIsntValid = false;
    this.userSearchKey = "";
    this.userList = [];
    this.filteredUserList = [];
    this.selectedUsers = []; 
    this.unSubscribe$ = new Subject<void>();   
  }

  //send user list
  save() : void { 
    if(String(this.group.name).length < 3 || String(this.group.name).length > 100){
      this.nameIsntValid = true;
      return;
    }else{
      this.nameIsntValid = false;
    }

    if(String(this.group.description).length > 250){
      this.descIsntValid = true;
      return;
    }else{
      this.descIsntValid = false;
    }

    if(this.group){
      if(this.group.id && !this.isNew && this.oldRec){
        this.group.isMain = this.oldRec.isMain;       
        of(this.group).pipe(
          takeUntil(this.unSubscribe$),
          map<GroupVM, GroupUpdateVM>(x => x)).subscribe(rec => {
          this.groupService.Update(rec, this.group.id).subscribe( () => {
            this.SendMessageToClients();
            this.returnBack();
          });
        }); 
      }else{
        this.group.isMain = false;
        of(this.group).pipe(
          takeUntil(this.unSubscribe$),
          map<GroupVM, GroupAddVM>(x => x)).subscribe(rec => {
            this.groupService.Add(rec).subscribe( () => {
              this.SendMessageToClients();
              this.returnBack();
            });
        });        
      }
    }
  }

  private SendMessageToClients(){    
    this.dataTrackingService.GroupUpdated(this.group.id);    
  }

  delete() : void {
    if(this.group.id && !this.isNew)
    {
      this.groupService.Delete(this.group.id).subscribe( () => {
        this.returnBack();
      });
    }
  }

  private returnBack() : void {
    this.router.navigate(['/']);
  }

  isPrivateClick() : void {
    if(!this.group.isPrivate){
      this.userSearchKey = "";
      this.filteredUserList = [];
      this.group.users = [];
    }
  }

  searchUser() : void {    
    if(this.userList && this.userList.length > 0){
      this.filteredUserList = this.userList.filter(a => 
        (a.userName.includes(this.userSearchKey) || a.displayName.includes(this.userSearchKey))
        && (this.group.users == null || !this.group.users.some(x => x == a.id)));

      this.userSearchKey = "";
    }else{
      this.addUser(this.CurrentUser.id);
    }
  }

  addUser(userId: string) : void {
    var user = this.userList.find(a=>a.id == userId);

    if(user){
      if(!this.group.users){
        this.group.users = [];
      }
      this.group.users.push(user.id);
      this.selectedUsers.push(user);

      this.filteredUserList = this.filteredUserList.filter(x=> x != user);
    }    
  }

  removeUser(userId: string) : void {      
    var user = this.userList.find(a=>a.id == userId);

    if(user){
      this.selectedUsers  = this.selectedUsers.filter(x=> x != user);    
      this.group.users = this.group.users.filter(x=> x != userId);    
    }
  }  
}
