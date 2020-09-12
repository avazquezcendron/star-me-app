import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MessageService } from './message.service';
import { StoryAdapter } from '../models/Story';
import { first, map, catchError, tap } from 'rxjs/operators';
import { Observable, Subject, of } from 'rxjs';
import { IResponseMessage } from '../contracts/IResponseMessage';
import { IBusinessEntity } from '../models/IBusinessEntity';
import { IResponseListDTO } from '../contracts/IResponseDTO';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class StoryService extends ApiService {
  constructor(http: HttpClient, messageService: MessageService, toastr: ToastrService) {
    super(http, messageService, toastr);
    super.entityName = 'stories';
    super.modelAdapter = new StoryAdapter();
  }

  public getStoriesByTag(tagId: string) {
    const url = `${this.getAPI_URL()}/tags`;
    const params = new HttpParams().set('tagId', tagId);
    return this.http.get(url, { params }).pipe(
      map((response: IResponseListDTO) => {
        if (!response.succeeded) {
          response.messages.map((message: IResponseMessage) =>
            this._log(message.message)
          );
          return of([]);
        }
        return response.data;
      }),
      map((data: IBusinessEntity[]) => {
        return data.map((d: IBusinessEntity) => this.modelAdapter.adapt(d));
      }),
      catchError(this.handleError<any>(`Get ${this.entityName}`))
    );
  }

  public getStoriesByTitle(title: string) {
    const url = `${this.getAPI_URL()}/title`;
    const params = new HttpParams().set('title', title);
    return this.http.get(url, { params }).pipe(
      map((response: IResponseListDTO) => {
        if (!response.succeeded) {
          response.messages.map((message: IResponseMessage) =>
            this._log(message.message)
          );
          return of([]);
        }
        return response.data;
      }),
      map((data: IBusinessEntity[]) => {
        return data.map((d: IBusinessEntity) => this.modelAdapter.adapt(d));
      }),
      catchError(this.handleError<any>(`Get ${this.entityName}`))
    );
  }
}
