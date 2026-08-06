using Shared.Response;

namespace Shared.Presentation;

public static class ResultMapper
{
    // Map Result to ApiResponse with custom success mapping
    public static ApiResponse MapToApiResponse<T>(
        this Result<T> result,
        Func<T, object>? dataMapper = null)
    {
        if (result.IsSuccess)
        {
            var data = dataMapper != null
                ? dataMapper(result.Value)
                : result.Value;

            return new ApiResponse
            {
                MessageCode = 200,
                MessageText = "Success",
                MessageData = data
            };
        }

        return new ApiResponse
        {
            MessageCode = GetStatusCode(result.Error),
            MessageText = result.Error.Description,
            MessageData = new
            {
                Error = result.Error.Description,
                Type = result.Error.GetType().Name
            }
        };
    }

    // Map with custom error handling
    public static ApiResponse MapToApiResponseWithCustomError<T>(
        this Result<T> result,
        Func<Error, object> errorMapper)
    {
        if (result.IsSuccess)
        {
            return new ApiResponse
            {
                MessageCode = 200,
                MessageText = "Success",
                MessageData = result.Value
            };
        }

        return new ApiResponse
        {
            MessageCode = GetStatusCode(result.Error),
            MessageText = result.Error.Description,
            MessageData = errorMapper(result.Error)
        };
    }

    private static int GetStatusCode(Error error)
    {
        return error.Description switch
        {
            var desc when desc.Contains("not found", StringComparison.OrdinalIgnoreCase) => 404,
            var desc when desc.Contains("validation", StringComparison.OrdinalIgnoreCase) => 400,
            var desc when desc.Contains("unauthorized", StringComparison.OrdinalIgnoreCase) => 401,
            var desc when desc.Contains("forbidden", StringComparison.OrdinalIgnoreCase) => 403,
            var desc when desc.Contains("conflict", StringComparison.OrdinalIgnoreCase) => 409,
            _ => 500
        };
    }
}