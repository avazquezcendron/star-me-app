import { Component, OnInit, Input } from '@angular/core';
import { TagService } from '../../services/tag.service';
import { Tag } from '../../models/Tag';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styles: [
  ]
})
export class TagsComponent implements OnInit {
  @Input() public tags: Tag[];

  constructor(public tagService: TagService) {}

  ngOnInit(): void {
    if(!this.tags) {
      this.tagService.getAll().subscribe((tags: Tag[]) => this.tags = tags);
    }
  }

  onClickTag() {

  }
}
