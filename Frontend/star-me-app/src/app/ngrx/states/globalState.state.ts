export enum GlobalStateMode{
  NEW = 'new',
  EDIT = 'edit',
  BROWSE = 'browse',
}

export interface IGlobalState{
  globalMode: GlobalStateMode;
}
