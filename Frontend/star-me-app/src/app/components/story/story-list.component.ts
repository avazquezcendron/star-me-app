import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Story } from '../../models/Story';
import { StoryService } from '../../services/story.service';
import { ActivatedRoute } from '@angular/router';
import { IResponseListDTO } from '../../contracts/IResponseDTO';
import { takeWhile } from 'rxjs/internal/operators/takeWhile';

@Component({
  selector: 'app-story-list',
  templateUrl: './story-list.component.html',
  styles: [],
})
export class StoryListComponent implements OnInit, OnDestroy {
  private alive = true;
  dataLoaded: boolean;
  stories: Story[];
  pageNumber = 1;
  pageSize = 4;
  numOfPages = 1;
  tagId: string;
  title: string;

  constructor(
    public storyService: StoryService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((data) => {
      this.tagId = data.tagId;
      this.title = data.title;
      if (this.tagId) {
        this.storyService
          .getStoriesByTag(this.tagId)
          .subscribe((stories: Story[]) => {
            this.stories = stories;
            this.dataLoaded = true;
            this.pageNumber = 1;
          });
      } else if (this.title) {
        this.storyService
          .getStoriesByTitle(this.title)
          .subscribe((stories: Story[]) => {
            this.stories = stories;
            this.dataLoaded = true;
            this.pageNumber = 1;
          });
      } else if (this.numOfPages >= this.pageNumber) {
        this.storyService
          .getAllPaged(this.pageNumber, this.pageSize)
          .subscribe((response: IResponseListDTO) => {
            if (response && response.succeeded) {
              this.stories = response.data;
              this.numOfPages = Math.ceil(
                response.totalListCount / this.pageSize
              );
              this.pageNumber++;
            }
            this.dataLoaded = true;
          });
      }
    },
    takeWhile(() => this.alive));
  }

  ngOnDestroy() {
    this.alive = false;
  }

  changePage() {
    if (this.numOfPages >= this.pageNumber) {
      this.dataLoaded = false;
      this.storyService
        .getAllPaged(this.pageNumber, this.pageSize)
        .subscribe((response: IResponseListDTO<Story>) => {
          // this.stories = this.stories.concat(response.data);
          this.stories = [...this.stories, ...response.data];
          this.pageNumber++;
          this.dataLoaded = true;
        });
    }
  }

  changePageVisible() {
    return (
      this.dataLoaded &&
      !this.tagId &&
      !this.title &&
      this.numOfPages >= this.pageNumber
    );
  }
}
