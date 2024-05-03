namespace AnVatCanThoWeb.Common;

public class GeminiSettings
{
    public const string SectionName = "GeminiSettings";
    
    public string ApiKey { get; init; } = string.Empty;
    
    public string TextBaseUrl { get; init; } = string.Empty;
}