using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGW_second_task.Models
{
    public class Load
    {
        public int Id { get; set; }
        public int Destination { get; set; }
        public static int NumberOfDestinations {get; set;}
        public static List<int> ListOfDestinations {get; set;}
        public bool FailedLoad { get; set; } = false;
    }
}