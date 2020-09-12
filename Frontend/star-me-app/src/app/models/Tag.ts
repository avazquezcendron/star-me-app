import { IBusinessEntity } from './IBusinessEntity';
import { Injectable } from '@angular/core';
import { IModelAdapter } from './IModelAdapter';

export class Tag implements IBusinessEntity<number> {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root',
})
export class TagAdapter implements IModelAdapter<Tag> {
  public adapt(item: any): Tag {
    const tag = new Tag();
    tag.id = item.id;
    tag.name = item.name;
    return tag;
  }
}
