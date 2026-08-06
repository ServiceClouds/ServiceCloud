namespace Shared.Presentation;

public class ApiResponse
{
    public int MessageCode { get; set; }

    public string MessageText { get; set; } = string.Empty;

    public object? MessageData { get; set; }
}

public class ApiResponseLegacy
{
    public int MessageCode { get; set; }

    public string MessageText { get; set; } = string.Empty;

    public object? MessageData { get; set; }
}