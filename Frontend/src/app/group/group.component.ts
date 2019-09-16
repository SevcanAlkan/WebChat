import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupService } from '@services/GroupService';
import { Group } from '@models/Group';
import { APIResultVM } from '@models/APIResultVM';

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

  constructor(private groupService :GroupService, private router: Router, private route: ActivatedRoute) {

   }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id') || null;        

    if(this.id){      
      this.groupService.GetById(this.id).subscribe((item: APIResultVM) => {     
        if(item.rec) {
          this.group = item.rec;
          this.oldRec = item.rec;
          this.isNew = false;  
          
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
}
