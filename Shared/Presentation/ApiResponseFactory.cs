using Shared.Response;

namespace Shared.Presentation;

public static class ApiResponseFactory
{
    public static ApiResponse CreateResponse<T>(Result<T> result)
    {
        return result.ToApiResponse();
    }

    public static ApiResponse CreateResponse(Result result)
    {
        return result.ToApiResponse();
    }

    public static ApiResponseLegacy CreateLegacyResponse<T>(Result<T> result)
    {
        return result.ToLegacyApiResponse();
    }
}