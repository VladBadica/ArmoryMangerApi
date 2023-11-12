namespace ArmoryManagerApi.ViewModels;

public record LoginReqVM
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}
