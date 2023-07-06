using System.ComponentModel.DataAnnotations;

namespace BankTransaction.DTO.AcoountDTO
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    
}
}
