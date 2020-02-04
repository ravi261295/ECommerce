using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.ModelsDB
{
    public class ProductDB
    {
        [Key]
        public int ProductID { get; set; }

        public string ModelNumber { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public int DeliveryTime { get; set; }

        public int AvailableQuantity { get; set; }

    }
}
