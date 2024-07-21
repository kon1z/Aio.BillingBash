namespace Aio.BillingBash.Components.ViewModels
{
    public class LoginResultDto
    {
        public LoginResultDto()
        {
            IsSuccess = true;
        }

        public LoginResultDto(params string[] errorComment)
        {
            IsSuccess = false;
            ErrorComment = errorComment.ToList();
        }

        public bool IsSuccess { get; set; }
        public List<string> ErrorComment { get; set; }
    }
}
