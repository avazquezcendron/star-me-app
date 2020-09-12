import { IBusinessEntity } from './IBusinessEntity';

export class User implements IBusinessEntity<number> {
  id: number;
  username: string;
  password: string;
  email: string;
}
