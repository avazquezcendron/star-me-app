import { IBusinessEntity } from './IBusinessEntity';
import { IAuditInfo } from './IAuditInfo';
import { Tag } from './Tag';
import { IModelAdapter } from './IModelAdapter';
import { Injectable } from '@angular/core';

export class Story implements IBusinessEntity<number> {

  constructor() {
    this.auditInfo = { createdAt: new Date().toString(), state: 'I' };
    this.tags = [];
  }

  id: number;
  auditInfo?: IAuditInfo;
  title: string;
  summary: string;
  content: string;
  tags: Tag[];
}

@Injectable({
  providedIn: 'root',
})
export class StoryAdapter implements IModelAdapter<Story> {
  public adapt(item: any): Story {
    const story = new Story();
    story.id = item.id;
    story.auditInfo = item.auditInfo;
    story.title = item.title;
    story.summary = item.summary;
    story.content = item.content;
    story.tags = item.tags;
    return story;
  }
}
