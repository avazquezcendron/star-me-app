export interface IModelAdapter<IBusinessEntity> {
  adapt(item: any): IBusinessEntity;
}
