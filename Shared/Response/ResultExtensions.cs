// Shared/Response/ResultExtensions.cs
using Microsoft.AspNetCore.Http;
using Shared.Presentation;
using System.Text.Json;

namespace Shared.Response;

public static class ResultExtensions
{
    // Generic Result<T> to ApiResponse
    public static ApiResponse ToApiResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new ApiResponse
            {
                MessageCode = 200, // Default success code
                MessageText = "Success",
                MessageData = result.Value,
                Result = result.Value
            };
        }

        return new ApiResponse
        {
            MessageCode = GetErrorCode(result.Error),
            MessageText = result.Error.Description,
            MessageData = new { Error = result.Error.Description },
            Result = null
        };
    }

    // Non-generic Result to ApiResponse
    public static ApiResponse ToApiResponse(this Result result)
    {
        if (result.IsSuccess)
        {
            return new ApiResponse
            {
                MessageCode = 200,
                MessageText = "Success",
                MessageData = null,
                Result = null
            };
        }

        return new ApiResponse
        {
            MessageCode = GetErrorCode(result.Error),
            MessageText = result.Error.Description,
            MessageData = new { Error = result.Error.Description },
            Result = null
        };
    }

    // To Legacy ApiResponse
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
            MessageData = new { Error = result.Error.Description }
        };
    }

    // Convert Result to IResult (for Minimal APIs/Controllers)
    public static IResult ToApiResult<T>(this Result<T> result)
    {
        var response = result.ToApiResponse();
        var statusCode = result.IsSuccess ? 200 : GetHttpStatusCode(result.Error);

        return Results.Json(
            response,
            new JsonSerializerOptions { PropertyNamingPolicy = null },
            statusCode: statusCode
        );
    }

    public static IResult ToApiResult(this Result result)
    {
        var response = result.ToApiResponse();
        var statusCode = result.IsSuccess ? 200 : GetHttpStatusCode(result.Error);

        return Results.Json(
            response,
            new JsonSerializerOptions { PropertyNamingPolicy = null },
            statusCode: statusCode
        );
    }

    public static IResult ToLegacyApiResult<T>(this Result<T> result)
    {
        var response = result.ToLegacyApiResponse();
        var statusCode = result.IsSuccess ? 200 : GetHttpStatusCode(result.Error);

        return Results.Json(
            response,
            new JsonSerializerOptions { PropertyNamingPolicy = null },
            statusCode: statusCode
        );
    }

    // Helper methods
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

    private static int GetHttpStatusCode(Error error)
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