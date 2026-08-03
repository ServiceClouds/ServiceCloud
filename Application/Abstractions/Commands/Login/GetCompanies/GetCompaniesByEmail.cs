using Application.Abstractions.Queries;

namespace Application.Abstractions.Commands.Login.GetCompanies;

public sealed class GetCompaniesByEmailQuery
    : IQuery<List<CompanyLookupResponse>>
{
    public GetCompaniesByEmailQuery(string email)
    {
        Email = email;
    }

    public string Email { get; }
}