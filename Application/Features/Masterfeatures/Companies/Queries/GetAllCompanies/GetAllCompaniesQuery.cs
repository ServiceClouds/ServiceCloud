using Domain.Entities.ServiceCloud;
using MediatR;
using Shared.Response;

namespace Application.Features.Masterfeatures.Companies.Queries.GetAllCompanies;

public sealed record GetAllCompaniesQuery
    : IRequest<Result<List<Company>>>;