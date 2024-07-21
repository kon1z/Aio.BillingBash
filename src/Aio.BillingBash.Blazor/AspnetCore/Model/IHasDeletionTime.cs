namespace Aio.BillingBash.AspnetCore.Model;

public interface IHasDeletionTime
{
    DateTime? DeletionTime { get; set; }
}