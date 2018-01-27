using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class Person
    {
        public int ID { get; set; }
        public string NIC { get; set; }
        public string Gender { get; set; }
        public Address Address { get; set; }
        public DateTime JoinedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int IsActive { get; set; }
        public Credentials UserCredentials { get; set; }
    }
}
