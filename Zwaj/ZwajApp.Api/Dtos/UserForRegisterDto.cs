using System.ComponentModel.DataAnnotations;

namespace ZwajApp.Api.Dtos
{
    public class UserForRegisterDto
    {    [Required]
        public string UserName { get; set; }
[StringLength(8,MinimumLength=4,ErrorMessage="يجب ان تكون كلمة المرور اكبرمن 4 حروف واصغر من 8")]
        public string Password { get; set; }
    }
}