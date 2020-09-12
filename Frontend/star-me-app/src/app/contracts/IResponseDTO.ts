import { IResponseMessage } from './IResponseMessage';
import { IBusinessEntity } from '../models/IBusinessEntity';

export interface IResponseDTO {

  succeeded: boolean;
  messages: IResponseMessage[];
}

export interface IResponseValueDTO<T = any> extends IResponseDTO{
  data: T;
}


export interface IResponseListDTO<T = any> extends IResponseDTO{
  data: T[];
  pageNumber?: number;
  pageSize?: number;
  totalListCount?: number;
}
