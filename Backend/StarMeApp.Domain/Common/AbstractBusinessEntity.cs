namespace StarMeApp.Domain.Common
{
    public interface IBusinessEntity<TId>
    {
        TId Id { get; set; }
    }

    public abstract class AbstractBusinessEntity<TId>: IBusinessEntity<TId>
    {
        public virtual TId Id { get; set; }
    }
}
