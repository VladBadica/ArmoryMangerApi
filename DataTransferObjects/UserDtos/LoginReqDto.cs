namespace ArmoryManagerApi.DataTransferObjects.UserDtos;
public class LoginReqDto
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}
