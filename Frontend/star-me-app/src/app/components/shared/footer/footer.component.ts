import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styles: [
  ]
})
export class FooterComponent implements OnInit {

  public get currentYear(): number {
    return new Date().getFullYear();
  }

  constructor() { }

  ngOnInit(): void {
  }

}
