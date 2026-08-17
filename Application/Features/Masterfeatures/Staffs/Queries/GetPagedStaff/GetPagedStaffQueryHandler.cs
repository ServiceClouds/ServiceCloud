using Application.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Abstractions.Repositories.Common;
using Shared.Response;

namespace Application.Features.Masterfeatures.Staffs.Queries.GetPagedStaff;

public sealed class GetPagedStaffQueryHandler
    : IRequestHandler<
        GetPagedStaffQuery,
        Result<PagedResponse<Staff>>>
{
    private readonly IGenericRepository<Staff> _staffRepository;

    public GetPagedStaffQueryHandler(
        IGenericRepository<Staff> staffRepository)
    {
        _staffRepository = staffRepository;
    }

    public async Task<Result<PagedResponse<Staff>>> Handle(
        GetPagedStaffQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Staff> query =
            _staffRepository.GetAll();

        if (!string.IsNullOrWhiteSpace(
            request.Request.Search))
        {
            query = query.Where(x =>
                x.FirstName.Contains(
                    request.Request.Search) ||

                (x.LastName != null &&
                 x.LastName.Contains(
                     request.Request.Search)) ||

                x.Email.Contains(
                    request.Request.Search));
        }

        var totalRecords = await query.CountAsync(
            cancellationToken);

        var staff = await query
            .Skip(
                (request.Request.PageNumber - 1)
                * request.Request.PageSize)
            .Take(request.Request.PageSize)
            .ToListAsync(cancellationToken);

        var response = new PagedResponse<Staff>
        {
            Items = staff,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalRecords = totalRecords,
            TotalPages =
                (int)Math.Ceiling(
                    (double)totalRecords /
                    request.Request.PageSize)
        };

        return Result<PagedResponse<Staff>>.Success(
            response);
    }
}