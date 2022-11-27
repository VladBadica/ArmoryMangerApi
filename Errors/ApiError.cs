using System.Text.Json;

namespace ArmoryManagerApi.Errors;

public class ApiError
{
    public int ErrorStatus { get; set; }

    public string ErrorMessage { get; set; }

    public ApiError(int errorStatus, string errorMessage) { 
        ErrorStatus = errorStatus;
        ErrorMessage = errorMessage;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
