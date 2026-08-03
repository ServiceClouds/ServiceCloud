using Application.Abstractions.Data;

namespace Persistence.Data
{
    public class LazyApplicationDbContext
    {
        private readonly ITenantDbContextFactory _factory;

        private IApplicationDbContext? _context;
        private int? _companyId;

        public LazyApplicationDbContext(
            ITenantDbContextFactory factory)
        {
            _factory = factory;
        }

        public async Task<IApplicationDbContext> GetAsync(int companyId)
        {
            // Create a new tenant DbContext only if it doesn't exist
            // or if the requested company is different.
            if (_context == null || _companyId != companyId)
            {
                _context = await _factory.CreateAsync(companyId);
                _companyId = companyId;
            }

            return _context;
        }
    }
}