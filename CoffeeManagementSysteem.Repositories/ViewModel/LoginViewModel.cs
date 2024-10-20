using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementSystem.Repositories.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name ="User Name")]
        [StringLength(256)]
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember me")]
        public bool Rememberme { get; set; }
    }
}
