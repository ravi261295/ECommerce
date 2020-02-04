using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.ModelsDB
{
    public class UserProductDBModel
    {
        [Key]
        public int UserID { get; set; }

        public string Email { get; set; }

        public string ModelNumber { get; set; }

        public int RequiredQuantity { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public int DeliveryTime { get; set; }
    }
}
