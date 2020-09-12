export interface IAuditInfo {
  createdAt?: string;
  state?: 'I' | 'U' | 'D';
  updatedAt?: string;
  user?: string;
}
