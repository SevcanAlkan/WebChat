import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupService } from '@services/GroupService';
import { Group } from '@models/Group';
import { APIResultVM } from '@models/APIResultVM';
import { UserService } from '@services/UserService';
import { UserListVM, User } from '@models/User';
import { AuthenticationService } from '@services/AuthenticationService';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {
  private group: Group = new Group();
  private oldRec: Group;
  private isNew: boolean = false;
  private id: string;
  private isDeleteVisible: boolean = false;

  private nameIsntValid:boolean = false;
  private descIsntValid:boolean = false;

  private userSearchKey: string = "";
  private userList: UserListVM[] = [];  
  private filteredUserList: UserListVM[] = [];  
  private selectedUsers: UserListVM[] = [];  

  private CurrentUser: User;
  
  constructor(private groupService : GroupService, private userService : UserService,
     private router: Router, private route: ActivatedRoute,
     private authenticationService: AuthenticationService) {
      this.authenticationService.currentUser.subscribe(x => this.CurrentUser = x); 
   }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id') || null;        

    this.userService.getUserList().subscribe(x => {
      x.forEach((item: UserListVM) => {    
        this.userList.push(item);                
      });  
    });

    if(this.id){      
      this.groupService.GetById(this.id).subscribe((item: APIResultVM) => {     
        if(item.rec) {
          this.group = item.rec;
          this.oldRec = item.rec;
          this.isNew = false;   

          this.groupService.GetUsers(this.id).subscribe(x => {
            if(x && x.length>0){
              this.group.users  = x;  
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

    
  }

  ngOnDestroy() {
  }

  //send user list
  save(){ 
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
        this.groupService.Update(this.group).subscribe( () => {
          this.returnBack();
        });
      }else{
        this.group.isMain = false;
        this.groupService.Add(this.group).subscribe( () => {
          this.returnBack();
        });
      }
    }
  }

  delete(){
    if(this.group.id && !this.isNew)
    {
      this.groupService.Delete(this.group.id).subscribe( () => {
        this.returnBack();
      });
    }
  }

  returnBack(){
    this.router.navigate(['/']);
  }

  isPrivateClick(){
    if(!this.group.isPrivate){
      this.userSearchKey = "";
      this.filteredUserList = [];
      this.group.users = [];
    }
  }

  searchUser(){    
    if(this.userList && this.userList.length>0){
      this.filteredUserList = this.userList.filter(a => 
        (a.userName.includes(this.userSearchKey) || a.displayName.includes(this.userSearchKey))
        && (this.group.users == null || !this.group.users.some(x => x == a.id)));

      this.userSearchKey = "";
    }else{
      this.addUser(this.CurrentUser.id);
    }
  }

  addUser(userId){
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

  removeUser(userId){      
    var user = this.userList.find(a=>a.id == userId);

    if(user){
      this.selectedUsers  = this.selectedUsers.filter(x=> x != user);    
      this.group.users = this.group.users.filter(x=> x != userId);    
    }
  }  
}
