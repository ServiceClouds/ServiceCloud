namespace Domain.Entities.ServiceCloud;

public class StaffToken
{
    private StaffToken()
    {
    }

    public long StaffTokenId { get; private set; }

    public int StaffLoginId { get; private set; }

    public string? AccessToken { get; private set; }

    public string? RefreshToken { get; private set; }

    public DateTime? AccessTokenExpiry { get; private set; }

    public DateTime? RefreshTokenExpiry { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public static StaffToken Create(
        int staffLoginId,
        string accessToken,
        DateTime? accessTokenExpiry = null,
        string? refreshToken = null,
        DateTime? refreshTokenExpiry = null)
    {
        return new StaffToken
        {
            StaffLoginId = staffLoginId,
            AccessToken = accessToken,
            AccessTokenExpiry = accessTokenExpiry,
            RefreshToken = refreshToken,
            RefreshTokenExpiry = refreshTokenExpiry,
            CreatedOn = DateTime.UtcNow
        };
    }
}