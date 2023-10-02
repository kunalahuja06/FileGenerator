// update namespace
namespace YOUR_NAMESPACE
{
    public class UnitOfWork:IUnitOfWork, IDisposable
    {
        // update context
        private readonly Context _context;
        public UnitOfWork(Context context)
        {
            _context = context;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async void SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        // add repositories here
    }
}
