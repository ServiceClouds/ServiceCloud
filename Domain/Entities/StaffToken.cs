
namespace Domain.Entities;



public partial class StaffToken
{
    private StaffToken() { }
    public long StaffTokenId { get; private set; }

    public int StaffLoginId { get; private set; }

    public DateTime? AccessTokenExpiry { get; private set; }

    public int? AppSourceTypeId { get;  private set; }

    public DateTime? CreatedOn { get; private set; }
}
