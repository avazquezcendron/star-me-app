import * as globalStateMode from '../actions/globalState.actions';
import { IGlobalState, GlobalStateMode } from '../states/globalState.state';

const initialState: IGlobalState = {
  globalMode: GlobalStateMode.BROWSE
}

export function globalStoreReducer(
  state = initialState,
  action: globalStateMode.Actions
): IGlobalState {
  if (action.type === globalStateMode.CHANGE_MODE) {
    return action.payload;
  } else {
    return state;
  }
}
