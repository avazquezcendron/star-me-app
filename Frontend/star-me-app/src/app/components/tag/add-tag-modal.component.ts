import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Tag } from '../../models/Tag';
import { TagService } from '../../services/tag.service';

@Component({
  selector: 'app-add-tag-modal',
  templateUrl: './add-tag-modal.component.html',
  styles: [],
})
export class AddTagModalComponent implements OnInit {
  tags: Tag[] = [];
  selectedTags: Tag[] = [];
  @Input() inputTags: Tag[] = [];
  tag: Tag = new Tag();

  @Output() public onSelectedTags: EventEmitter<Tag[]> = new EventEmitter<Tag[]>();

  constructor(public tagService: TagService) {}

  ngOnInit(): void {
    this.selectedTags = [ ...this.inputTags ];
    this.tagService.getAll().subscribe((tags: Tag[]) => (this.tags = tags));
  }

  handleKeyboardEvent(event: KeyboardEvent) {
    if (event.code === 'Enter' && this.tag.name && this.tag.name.length > 0) {
      this.addNewTag();
     }
  }

  addNewTag() {
    this.addTag({ ...this.tag });
  }

  addTag(tag: Tag): void {
    if (!this.selectedTags.some((tag) => tag.name === this.tag.name)) {
      this.selectedTags.push(tag);
    }
  }

  removeTag(tag: Tag): void {
    const index = this.selectedTags.indexOf(tag);
    if (index !== -1) {
      this.selectedTags.splice(index, 1);
    }
  }

  onSaveChanges() {
    this.onSelectedTags.emit([...this.selectedTags]);
    // this.selectedTags = [];
  }
}
