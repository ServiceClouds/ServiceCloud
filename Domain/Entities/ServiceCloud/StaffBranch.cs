namespace Domain.Entities.ServiceCloud;

public class StaffBranch
{
    // Required by EF Core
    private StaffBranch()
    {
    }

    public int StaffBranchId { get; private set; }

    public int StaffLoginId { get; private set; }

    public int BranchId { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public static StaffBranch Create(
        int staffLoginId,
        int branchId)
    {
        return new StaffBranch
        {
            StaffLoginId = staffLoginId,
            BranchId = branchId,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        };
    }
    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }

}