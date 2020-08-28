namespace StarMeApp.Domain.Common
{
    public interface IBusinessEntity
    {

    }

    public interface IBusinessEntity<TId>: IBusinessEntity
    {
        TId Id { get; set; }
    }

    public abstract class AbstractBusinessEntity<TId>: IBusinessEntity<TId>
    {
        public virtual TId Id { get; set; }
    }
}
