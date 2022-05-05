import {Component, OnInit} from '@angular/core';
import {MessageVM} from '@app/Models/message';
import {SearchService} from '@app/services/SearchService';
import {ActivatedRoute, Router} from '@angular/router';
import {Group} from '@app/Models/group';
import {UserListVM} from '@app/Models/user';
import {GroupService} from '@app/services/GroupService';
import {UserService} from '@app/services/UserService';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  private searchResult: MessageVM[] = [];
  private searchText: string = "";

  private GroupList: Group[] = [];
  private UserList: UserListVM[] = [];

  constructor(private searchService: SearchService, private router: Router, private route: ActivatedRoute,
              private userService: UserService, private groupService: GroupService) {
  }

  ngOnInit() {
    this.searchText = this.route.snapshot.paramMap.get('key') || "";

    if (this.searchText == null || this.searchText == "" || String(this.searchText).length < 4) {
      this.returnBack();
    }

    this.loadUsers();
    this.loadGroups();

    this.searchService.Get(this.searchText).subscribe(x => {
      if (x && x.length > 0) {

        x.forEach((item) => {
          this.searchResult.push(item);
          console.log(item);
        });
      }
    });
  }

  returnBack() {
    this.router.navigate(['/']);
  }

  getGroupName(id) {
    var group = this.GroupList.find(item => item.id === id);
    if (group) {
      return group.name;
    } else {
      return "Undefined Group";
    }
  }

  getUserName(id) {
    var user = this.UserList.find(item => item.id === id);
    if (user) {
      return user.displayName;
    } else {
      return "Undefined User";
    }
  }

  loadUsers() {
    this.UserList = [];

    this.userService.getUserList().subscribe(x => {
      x.forEach((item: UserListVM) => {
        this.UserList.push(item);
      });
    });
  }

  loadGroups() {
    this.groupService.GetAll().subscribe(x => {
      x.forEach(item => {
        var group = new Group();
        group.id = item.id;
        group.name = item.name;

        this.GroupList.push(group);
      })
    });
  }
}
