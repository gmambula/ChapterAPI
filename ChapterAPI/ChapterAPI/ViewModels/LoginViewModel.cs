using System.ComponentModel.DataAnnotations;

namespace ChapterAPI.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="O email é obrigatório")]
        public string? email { get; set; }
        [Required(ErrorMessage = "A Senha é Obrigatória")]
        public string? senha { get; set; }
    }
}
