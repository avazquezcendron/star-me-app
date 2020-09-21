import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { first, map, catchError, tap } from 'rxjs/operators';
import { Observable, Subject, of } from 'rxjs';
import { MessageService } from './message.service';
import { TagAdapter } from '../models/Tag';
import { ToastrService } from 'ngx-toastr';
import { IResponseListDTO, IResponseValueDTO } from '../contracts/IResponseDTO';
import { IBusinessEntity } from '../models/IBusinessEntity';
import { IResponseMessage } from '../contracts/IResponseMessage';

@Injectable({
  providedIn: 'root',
})
export class TagService extends ApiService {
  constructor(http: HttpClient, messageService: MessageService, toastr: ToastrService) {
    super(http, messageService, toastr);
    super.entityName = 'tags';
    super.modelAdapter = new TagAdapter();
  }

  public getTagWithStories(tagId: string) {
    const url = `${this.getAPI_URL()}/${tagId}/stories`;
    return this.http.get(url).pipe(
      map((response: IResponseValueDTO) => {
        if (!response.succeeded) {
          response.messages.map((message: IResponseMessage) =>
            this._log(message.message)
          );
          return of([]);
        }
        return response.data;
      }),
      map((data: IBusinessEntity[]) => {
        return this.modelAdapter.adapt(data);
      }),
      catchError(this.handleError<any>(`Get ${this.entityName}`))
    );
  }
}
