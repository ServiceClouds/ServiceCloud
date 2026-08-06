// Shared/Response/Success.cs
namespace Shared.Response;

public sealed record Success
{
    public static readonly Success None = new(0, string.Empty);

    public int Code { get; }
    public string Description { get; }
    public SuccessType Type { get; }

    private Success(int code, string description, SuccessType type = SuccessType.Ok)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public static Success Ok(string description = "Operation completed successfully")
        => new(200, description, SuccessType.Ok);

    public static Success Created(string description = "Resource created successfully")
        => new(201, description, SuccessType.Created);

    public static Success Updated(string description = "Resource updated successfully")
        => new(200, description, SuccessType.Updated);

    public static Success Deleted(string description = "Resource deleted successfully")
        => new(204, description, SuccessType.Deleted);

    public static Success Found(string description = "Resource found")
        => new(200, description, SuccessType.Found);

    public static Success NoContent(string description = "No content")
        => new(204, description, SuccessType.NoContent);
}   