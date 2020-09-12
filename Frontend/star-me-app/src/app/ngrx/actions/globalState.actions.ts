import { Action } from '@ngrx/store';
import { IGlobalState } from '../states/globalState.state';

export const CHANGE_MODE = '[GlobalState] Change-Mode';

export class ChangeMode implements Action {
  readonly type = CHANGE_MODE;

  constructor(public payload: IGlobalState) {}
}

export type Actions = ChangeMode;
