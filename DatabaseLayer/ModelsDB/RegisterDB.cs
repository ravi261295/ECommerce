using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.ModelsDB
{
    public class RegisterDB
    {
        public RegisterDB()
        {
            IsAdmin = false;
        }

        [Key]
        public int RegisterID { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

    }
}