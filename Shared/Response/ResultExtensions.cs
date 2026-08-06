using Shared.Presentation;

namespace Shared.Response;

public static class ResultExtensions
{
    // Generic Result<T> -> ApiResponse
    public static ApiResponse ToApiResponse<T>(this Result<T> result)
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
            MessageCode = GetErrorCode(result.Error),
            MessageText = result.Error.Description,
            MessageData = new
            {
                Error = result.Error.Description
            }
        };
    }

    // Non Generic Result -> ApiResponse
    public static ApiResponse ToApiResponse(this Result result)
    {
        if (result.IsSuccess)
        {
            return new ApiResponse
            {
                MessageCode = 200,
                MessageText = "Success",
                MessageData = null
            };
        }

        return new ApiResponse
        {
            MessageCode = GetErrorCode(result.Error),
            MessageText = result.Error.Description,
            MessageData = new
            {
                Error = result.Error.Description
            }
        };
    }

    // Legacy Response
    public static ApiResponseLegacy ToLegacyApiResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new ApiResponseLegacy
            {
                MessageCode = 200,
                MessageText = "Success",
                MessageData = result.Value
            };
        }

        return new ApiResponseLegacy
        {
            MessageCode = GetErrorCode(result.Error),
            MessageText = result.Error.Description,
            MessageData = new
            {
                Error = result.Error.Description
            }
        };
    }

    private static int GetErrorCode(Error error)
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