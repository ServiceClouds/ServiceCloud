using Shared.Response;

namespace Shared.Presentation;

public static class ApiResponseFactory
{
    public static ApiResponse CreateResponse<T>(Result<T> result)
    {
        return ResultExtensions.ToApiResponse(result);
    }

    public static ApiResponse CreateResponse(Result result)
    {
        return ResultExtensions.ToApiResponse(result);
    }

    public static ApiResponseLegacy CreateLegacyResponse<T>(Result<T> result)
    {
        return ResultExtensions.ToLegacyApiResponse(result);
    }
}