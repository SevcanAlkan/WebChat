import { Component, AfterViewChecked, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements AfterViewChecked {
  @ViewChild('scrollMe', {static: false}) private myScrollContainer: ElementRef;

  constructor(){      
  }  

  ngOnInit() { 
    this.scrollToBottom();    
  } 

  ngAfterViewChecked() {        
    this.scrollToBottom();        
  }
  scrollToBottom(): void {
      try {
          this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
      } catch(err) { }                 
  }
}
