import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent, StoryComponent } from './components/index.pages';


const routes: Routes = [
  // { path: '', component: HomeComponent },
  { path: 'stories', component: HomeComponent  },
  { path: 'story', component: StoryComponent },
  { path: 'stories/:id', component: StoryComponent },
  { path: '', pathMatch: 'full', redirectTo: 'stories' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule],
})
export class AppRoutingModule { }
