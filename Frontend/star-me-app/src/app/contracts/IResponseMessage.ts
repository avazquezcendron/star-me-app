export interface IResponseMessage {
  kind: 'Fatal' | 'Error' | 'Info' | 'Warning' | 'Debug' | 'Other';
  message: string;
  timestamp?: string;
}
