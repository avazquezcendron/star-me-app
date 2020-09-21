import { IBusinessEntity } from './IBusinessEntity';
import { Injectable } from '@angular/core';
import { IModelAdapter } from './IModelAdapter';
import { Story } from './Story';

export class Tag implements IBusinessEntity<number> {

  constructor() {
    this.stories = [];
  }

  id: number;
  name: string;
  stories: Story[];

}

@Injectable({
  providedIn: 'root',
})
export class TagAdapter implements IModelAdapter<Tag> {
  public adapt(item: any): Tag {
    const tag = new Tag();
    tag.id = item.id;
    tag.name = item.name;
    tag.stories = item.stories;
    return tag;
  }
}
