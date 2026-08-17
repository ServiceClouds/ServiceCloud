//using Application.Abstractions.Data;
//using Shared.Response;

//namespace Persistence.Data
//{
//    public class LazyApplicationDbContext
//    {
//        private readonly ITenantDbContextFactory _factory;

//        private IApplicationDbContext? _context;
//        private int? _companyId;

//        public LazyApplicationDbContext(
//            ITenantDbContextFactory factory)
//        {
//            _factory = factory;
//        }

//        public async Task<Result<IApplicationDbContext>> GetAsync(int companyId)
//        {

//            if (_context == null || _companyId != companyId)
//            {
//                var contextResult = await _factory.CreateAsync(companyId);

//                if (contextResult.IsFailure)
//                {
//                    return Result<IApplicationDbContext>.Failure(contextResult.Error);
//                }

//                _context = contextResult.Value;
//                _companyId = companyId;
//            }

//            return Result<IApplicationDbContext>.Success(_context!);
//        }
//    }
//}