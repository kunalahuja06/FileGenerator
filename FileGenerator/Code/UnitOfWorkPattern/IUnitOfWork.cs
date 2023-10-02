// update namespace
namespace YOUR_NAMESPACE
{
    public interface IUnitOfWork: IDisposable
    {
        void SaveChanges();
        void SaveChangesAsync();
    }
}
