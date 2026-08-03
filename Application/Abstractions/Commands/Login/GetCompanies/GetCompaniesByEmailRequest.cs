using System.ComponentModel.DataAnnotations;

namespace Application.Abstractions.Commands.Login.GetCompanies;

public sealed class GetCompaniesByEmailRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}