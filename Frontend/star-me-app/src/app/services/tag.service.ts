import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient } from '@angular/common/http';
import { MessageService } from './message.service';
import { TagAdapter } from '../models/Tag';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class TagService extends ApiService {
  constructor(http: HttpClient, messageService: MessageService, toastr: ToastrService) {
    super(http, messageService, toastr);
    super.entityName = 'tags';
    super.modelAdapter = new TagAdapter();
  }
}
