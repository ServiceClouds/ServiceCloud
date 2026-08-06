namespace Domain.Entities.ServiceCloud;

public class StaffLoggedInBranch
{
    // Required by EF Core
    private StaffLoggedInBranch()
    {
    }

    public long StaffTokenId { get; private set; }

    public int StaffId { get; private set; }

    public int BranchId { get; private set; }

    public DateTime LoggedInDate { get; private set; }

    public static StaffLoggedInBranch Create(
        int staffId,
        int branchId)
    {
        return new StaffLoggedInBranch
        {
            StaffId = staffId,
            BranchId = branchId,
            LoggedInDate = DateTime.UtcNow
        };
    }

    public void ChangeBranch(int branchId)
    {
        BranchId = branchId;
        LoggedInDate = DateTime.UtcNow;
    }
}