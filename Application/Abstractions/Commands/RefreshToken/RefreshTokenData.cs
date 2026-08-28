namespace Application.Abstractions.Commands.Login.RefreshToken;

public sealed class RefreshTokenData
{
    public long StaffTokenId { get; init; }

    public int StaffLoginId { get; init; }

    public int StaffId { get; init; }

    public int CompanyId { get; init; }

    public int BranchId { get; init; }

    public string Email { get; init; } = string.Empty;

    public bool IsSuperAdmin { get; init; }

    public bool HasEnterpriseRole { get; init; }

    public string RefreshToken { get; init; } = string.Empty;

    public DateTime? RefreshTokenExpiry { get; init; }
}