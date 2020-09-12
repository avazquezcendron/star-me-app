import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IGlobalState, GlobalStateMode } from '../../../ngrx/states/globalState.state';
import { Store } from '@ngrx/store';
import { takeWhile } from 'rxjs/internal/operators/takeWhile';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styles: [],
})
export class NavBarComponent implements OnInit {
  searchTerm = '';
  globalStateMode: GlobalStateMode;
  constructor(private router: Router, private globalStore: Store<{ globalState: IGlobalState }>) {
    this.globalStore.subscribe((globalStore) => {
      this.globalStateMode = globalStore.globalState.globalMode;
    });
  }

  ngOnInit(): void {}

  applySearch(event: KeyboardEvent) {
    if (event.code === 'Enter') {
      this.router.navigate(['/'], { queryParams: { title: this.searchTerm } });
    }
  }
}
