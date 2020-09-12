import { Component } from '@angular/core';
import { PageInfoService } from './services/page-info.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public get DataLoaded(): boolean {
    return this.pageInfoService.DataLoaded;
  }
  constructor(private pageInfoService: PageInfoService) {
    // console.log(this.pageInforService.);
  }
}
