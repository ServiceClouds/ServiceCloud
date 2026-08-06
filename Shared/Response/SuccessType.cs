// Shared/Response/SuccessType.cs
namespace Shared.Response;

public enum SuccessType
{
    Ok = 200,
    Created = 201,
    Updated = 200,
    Deleted = 204,
    Found = 200,
    NoContent = 204
}