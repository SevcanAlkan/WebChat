import {Component, NgZone, OnInit} from '@angular/core';
import {Router} from '@angular/router';

import {Message, TempMessage} from '@app/Models/message';
import {Group} from '@app/Models/group';
import {User, UserListVM} from '@app/Models/user';

import {UserService} from '@services/UserService';
import {GroupService} from '@services/GroupService';
import {AuthenticationService} from '@services/AuthenticationService';
import {ChatService} from '@services/ChatService';
import {MessageService} from '@services/MessageService';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  CurrentGroup: Group = new Group; //Already selected group.
  messages: Message[] = [];
  tempMessage: string = ""; //Current message text, from input.
  NavbarToggle = true;
  private CurrentUser: User; //Current logged in user.
  private GroupList: Group[] = [];
  private UserList: UserListVM[] = [];
  private tempMessages: TempMessage[] = []; //Which messages have written but didn't send.
  private message: Message; //Message model for send to API.
  private searchText: string = "";

  constructor(private _Route: Router,
              private authenticationService: AuthenticationService,
              private userService: UserService,
              private groupService: GroupService,
              private messageService: MessageService,
              private chatService: ChatService,
              private _ngZone: NgZone) {
    this.authenticationService.currentUser.subscribe(x => this.CurrentUser = x);
    this.chatService.openConnection(this.CurrentUser.id);
    this.subscribeToEvents();
  }

  get sortGroups() {
    this.GroupList = this.GroupList.sort((a, b) => a.name.localeCompare(b.name)).sort(a => {
      if (a.isMain) {
        return -1;
      } else {
        return 1;
      }
    });
    return this.GroupList;
  }

  get sortMessages() {
    return this.messages.sort((a, b) => {
      return <any>new Date(a.date) - <any>new Date(b.date);
    });
  }

  ngOnInit() {
    this.chatService.hubIsReady.subscribe(() => {
      //Load groups
      this.getGroups();
      //Load old messages
      this.getMessages();
      //Load info of all registered users
      this.loadUsers();
    });

  }

  logout() {
    this.authenticationService.logout();
    this._Route.navigate(['/login']);
  }

  //Functions for load data
  getGroups() {
    var groups: Observable<Group[]>;

    if (this.CurrentUser.isAdmin) {
      groups = this.groupService.GetAll();
    } else {
      groups = this.groupService.GetByUserId(this.CurrentUser.id);
    }

    groups.forEach(data => {
      data.forEach(item => {
        var group = new Group();
        group.id = item.id;
        group.name = item.name;
        group.description = item.description;
        group.isMain = item.isMain;
        group.isPrivate = item.isPrivate;
        group.users = [];

        if (group.isMain) {
          this.CurrentGroup = group;
        }

        this.GroupList.push(group);
      })
    }).then(() => {
      this.selectGroup(this.CurrentGroup.id, true);
    });
  }

  getMessages() {
    this.messages = [];

    if ((this.CurrentGroup && this.CurrentGroup.id)) {

      this.messageService.getByGroupId(this.CurrentGroup.id).forEach(data => {
        data.forEach(item => {

          if (item.userId === this.CurrentUser.id) {
            item.type = "sent";
          } else {
            item.type = "received";
          }
          this.messages.push(item);
        })
      });

      var _tempMessage = this.tempMessages.find(a => a.groupId === this.CurrentGroup.id);
      if (_tempMessage && _tempMessage.text) {
        this.tempMessage = _tempMessage.text;
      }
    }
  }

  loadUsers() {
    var users = this.userService.getUserList();
    this.UserList = [];
    if (users) {
      users.forEach((item: UserListVM[]) => {
        item.forEach(user => {
          this.UserList.push(user);
        })
      });
    }
  }

  //-------------------------

  selectGroup(id, loadAnyWay: boolean = false) {
    if (id == null || (id == this.CurrentGroup.id && !loadAnyWay)) {
      return;
    } else if (this.CurrentGroup && id === this.CurrentGroup.id && !loadAnyWay) {
      return;
    }

    var group = this.GroupList.find(item => item.id === id);
    if (group == null) {
      return;
    }

    if (this.tempMessage != "") {
      this.clearTempMessageOfGroup(this.CurrentGroup.id);
      var _tempMessage = new TempMessage();
      _tempMessage.text = this.tempMessage;
      _tempMessage.groupId = this.CurrentGroup.id;
      this.tempMessages.push(_tempMessage);
      this.tempMessage = "";
    }

    this.messages = [];
    this.CurrentGroup = group;
    this.chatService.updateGroupId(this.CurrentGroup.id);
    this.getMessages();
  }

  sendMessage(message: Message) {
    if (this.tempMessage) {
      var text = "";
      if (this.tempMessage.length >= 500) {
        text = this.tempMessage.substring(0, 499);
      } else {
        text = this.tempMessage;
      }

      this.message = new Message();
      this.message.userId = this.CurrentUser.id;
      this.message.groupId = this.CurrentGroup.id;
      this.message.type = "sent";
      this.message.text = text;
      this.message.date = new Date();

      this.messages.push(this.message);
      this.chatService.sendMessage(this.message);

      //Clear temp message
      this.tempMessage = "";
      this.clearTempMessageOfGroup(this.message.groupId);
    }
  }

  clearTempMessageOfGroup(groupId) {
    let _sentMessage = this.tempMessages.find(a => a.groupId === groupId);
    this.tempMessages = this.tempMessages.filter(obj => obj !== _sentMessage);
  }

  getUserName(id) {
    if (this.UserList) {
      if (id == this.CurrentUser.id) {
        return "You";
      }

      var user = this.UserList.find(item => item.id === id);
      if (user) {
        return user.displayName;
      } else {
        this.loadUsers();
        this.selectGroup(this.CurrentGroup.id, true);

        return "Undefined User";
      }
    }
  }

  showProfile(id) {
    if (id == null || id == '') {
      return;
    } else if (id == this.CurrentUser.id) {
      this._Route.navigate(['profile']);
    }
    this._Route.navigate(['profile', id]);
  }

  getMessageCountForGroup(groupId) {
    //   if(this.Mesa){
    //     var user = this.UserList.find(item=>item.id===id);
    //     if(user){
    //       return user.displayName;
    //     }else{
    //       return "Undefined User";
    //     }
    // }
    return 0; //Add feature to get unreaded messages
  }

  editGroup() {
    this._Route.navigate(['group', this.CurrentGroup.id]);
  }

  search() {
    if (this.searchText == null || this.searchText == "" || String(this.searchText).length < 4) {
      return;
    } else {
      this._Route.navigate(['search', this.searchText]);
    }
  }

  toggleNavBar() {
    if (this.NavbarToggle) {
      this.NavbarToggle = false;
    } else {
      this.NavbarToggle = true;
    }
  }

  private subscribeToEvents() {
    this.chatService.connectionEstablished.subscribe(x => {
      if (x) {
        this.chatService.messageReceived.subscribe((message: Message) => {
          this._ngZone.run(() => {
            if (message.userId !== this.CurrentUser.id) {
              message.type = "received";
              this.messages.push(message);
            }
          });
        });
      }
    });
  };
}
