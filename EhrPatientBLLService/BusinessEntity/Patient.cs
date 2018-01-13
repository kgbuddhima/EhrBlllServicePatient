using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class Patient:Person
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PIN { get; set; }
        public DateTime Birthday { get; set; }
    }
}
