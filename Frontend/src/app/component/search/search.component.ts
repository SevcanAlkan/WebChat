import { Component, OnInit } from '@angular/core';
import { MessageSearchVM } from '@models/Message';
import { SearchService } from '@services/SearchService';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupVM } from '@models/Group';
import { UserListVM } from '@models/User';
import { GroupService } from '@services/GroupService';
import { UserService } from '@services/UserService';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  private unSubscribe$: Subject<void>;
  
  private searchResult: MessageSearchVM[];
  private searchText: string;

  private GroupList: GroupVM[]; 
  private UserList: UserListVM[];  
  
  constructor(private searchService :SearchService, private router: Router, private route: ActivatedRoute,
    private userService: UserService, private groupService :GroupService) { 
      this.loadDefaultValues(); 
    }

  ngOnInit() {
    this.searchText = this.route.snapshot.paramMap.get('key') || "";

    if(this.searchText == null || this.searchText == "" || String(this.searchText).length < 4){
      this.returnBack();
    }

    this.loadUsers();
    this.loadGroups();

    this.searchService.Get(this.searchText)
      .pipe(takeUntil(this.unSubscribe$))
      .subscribe(x => {
        if(x && x.length > 0){
          
          x.forEach((item) => {
            this.searchResult.push(item);
            console.log(item);
          });
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
    this.searchResult = [];
    this.searchText = "";
    this.GroupList = [];
    this.UserList = [];

    this.unSubscribe$ = new Subject<void>();   
  }

  private returnBack() : void {
    this.router.navigate(['/']);
  }

  getGroupName(id: string) : string {
    var group = this.GroupList.find(item=>item.id===id);
    if(group){
      return group.name;
    }else{
      return "Undefined Group";
    }
  }

  getUserName(id: string) : string{
    var user = this.UserList.find(item=>item.id===id);
    if(user){
      return user.displayName;
    }else{
      return "Undefined User";
    }
  }

  private loadUsers() : void{
    this.UserList = [];

    this.userService.getUserList()
      .pipe(takeUntil(this.unSubscribe$))
      .subscribe(x => { 
        x.forEach((item: UserListVM) => {        
            this.UserList.push(item);      
        });      
      });      
  }
  
  private loadGroups() : void {
    this.groupService.GetAll()
      .pipe(takeUntil(this.unSubscribe$))
      .subscribe(x => {
        x.forEach(item=>{
          var group = new GroupVM();
          group.id = item.id;
          group.name = item.name;
          
          this.GroupList.push(group);
        })
      });
  } 
}
