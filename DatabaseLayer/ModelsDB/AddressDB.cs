using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.ModelsDB
{
    public class AddressDB
    {
        [Key]
        public int AddressID { get; set; }

        public string Email { get; set; }

        public string DeliveryAddress { get; set; }

    }
}
