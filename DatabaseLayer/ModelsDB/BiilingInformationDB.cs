using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.ModelsDB
{
    public class BiilingInformationDB
    {
        [Key]
        public int BillingID { get; set; }

        public double TotalAmount { get; set; }

        public int ExpectedDeliveryTime { get; set; }

        public string Email { get; set; }
    }
}
