// Shared/Presentation/ApiResponseFactory.cs
using Microsoft.AspNetCore.Http;
using Shared.Response;
using System.Text.Json;

namespace Shared.Presentation;

public static class ApiResponseFactory
{
    public static IResult CreateResponse<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Results.Json(
                CreateSuccessResponse(result),
                new JsonSerializerOptions { PropertyNamingPolicy = null },
                statusCode: 200
            );
        }

        return Results.Json(
            CreateErrorResponse(result.Error),
            new JsonSerializerOptions { PropertyNamingPolicy = null },
            statusCode: GetStatusCode(result.Error)
        );
    }

    public static IResult CreateResponse(Result result)
    {
        if (result.IsSuccess)
        {
            return Results.Json(
                CreateSuccessResponse(),
                new JsonSerializerOptions { PropertyNamingPolicy = null },
                statusCode: 200
            );
        }

        return Results.Json(
            CreateErrorResponse(result.Error),
            new JsonSerializerOptions { PropertyNamingPolicy = null },
            statusCode: GetStatusCode(result.Error)
        );
    }

    private static ApiResponse CreateSuccessResponse<T>(Result<T> result)
    {
        return new ApiResponse
        {
            MessageCode = 200,
            MessageText = "Success",
            MessageData = result.Value,
            Result = result.Value
        };
    }

    private static ApiResponse CreateSuccessResponse()
    {
        return new ApiResponse
        {
            MessageCode = 200,
            MessageText = "Success",
            MessageData = null,
            Result = null
        };
    }

    private static ApiResponse CreateErrorResponse(Error error)
    {
        return new ApiResponse
        {
            MessageCode = GetStatusCode(error),
            MessageText = error.Description,
            MessageData = new { Error = error.Description },
            Result = null
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