import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { Observable, Subject, of } from 'rxjs';
import { first, map, catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment.prod';
import { IBusinessEntity } from '../models/IBusinessEntity';
import {
  IResponseValueDTO,
  IResponseListDTO,
  IResponseDTO,
} from '../contracts/IResponseDTO';
import { MessageService } from './message.service';
import { IModelAdapter } from '../models/IModelAdapter';
import { IResponseMessage } from '../contracts/IResponseMessage';
import { ToastrService } from 'ngx-toastr';

const API_URL = `${environment.apiUrl}`;

export abstract class ApiService {
  constructor(
    protected http: HttpClient,
    public messageService: MessageService,
    protected toastr: ToastrService
  ) {}

  protected entityName = '';
  protected modelAdapter: IModelAdapter<IBusinessEntity>;
  protected httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  protected getAPI_URL() {
    return `${API_URL}${this.entityName}`;
  }

  get(id: any) {
    const url = `${API_URL}${this.entityName}/${id}`;
    return this.http.get(url).pipe(
      map((response: IResponseValueDTO) => {
        if (!response.succeeded) {
          response.messages.map((message: IResponseMessage) =>
            this._log(message.message)
          );
          return of([]);
        }
        return this.modelAdapter.adapt(response.data);
      }),
      catchError(this.handleError<any>(`Get ${this.entityName}`))
    );
  }

  getAll() {
    const url = `${API_URL}${this.entityName}`;
    return this.http.get(url).pipe(
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

  getAllPaged(pageNumber, pageSize) {
    const url = `${API_URL}${this.entityName}`;
    const params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize);
    return this.http.get(url, { params }).pipe(
      map((response: IResponseListDTO) => {
        if (!response.succeeded) {
          response.messages.map((message: IResponseMessage) =>
            this._log(message.message)
          );
          return of([]);
        }
        return response;
      }),
      map((response: IResponseListDTO) => {
        const data = response.data.map((d: IBusinessEntity) =>
          this.modelAdapter.adapt(d)
        );
        response.data = data;
        return response;
      }),
      catchError(this.handleError<any>(`Get ${this.entityName}`))
    );
  }

  post(entity: IBusinessEntity) {
    const url = `${API_URL}${this.entityName}`;
    return this.http.post(url, entity, this.httpOptions).pipe(
      map((response: IResponseDTO) => {
        if (response.succeeded) {
          this.toastr.success('Saved successfully.', 'Success');
          return response;
        }
        this.toastr.error(
          `There was an error. ${response.messages.map((msg) => msg.message)}`,
          'Error'
        );
      }),
      catchError(this.handleError<any>(`Add ${this.entityName}`))
    );
  }

  put(entity: IBusinessEntity) {
    const url = `${API_URL}${this.entityName}/${entity.id}`;
    return this.http.put(url, entity).pipe(
      map((response: IResponseDTO) => {
        if (response && response.succeeded) {
          this.toastr.success('Updated successfully.', 'Success');
          return response;
        }
        this.toastr.error(
          `There was an error. ${response.messages.map((msg) => msg.message)}`,
          'Error'
        );
      }),
      catchError(this.handleError<any>(`Update ${this.entityName}`))
    );
  }

  delete(id: any) {
    const url = `${API_URL}${this.entityName}/${id}`;
    return this.http.delete(url).pipe(
      map((response: IResponseDTO) => {
        if (response && response.succeeded) {
          this.toastr.success('Deleted  successfully.', 'Success');
          return response;
        }
        this.toastr.error(
          `There was an error. ${response.messages.map((msg) => msg.message)}`,
          'Error'
        );
      }),
      catchError(this.handleError<any>(`Delete ${this.entityName}`))
    );
  }

  protected handleError<T>(operation = 'operation', result?: T) {
    return (error: HttpErrorResponse): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this._log(`${operation} failed: ${error.message}`);

      this.toastr.error(
        `There was an error. ${JSON.stringify(error.error.errors)}`,
        'Error'
      );

      // Let the app keep running by returning an empty result.
      return of(result);
    };
  }

  protected _log(message: string) {
    this.messageService.add(`Api Service: ${message}`);
  }
}
