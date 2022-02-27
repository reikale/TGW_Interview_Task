using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGW_second_task.Models
{

    public class Conveyor
    {
        public enum destinationSelectionStrategy
        {
            RoundRobin,
            Random
        }

        public List<List<int>> DestinationEndpoints { get; set; } = new List<List<int>>();

        public static destinationSelectionStrategy SelectedStratedy { get; set; }

        public static int ConsecutiveLoads { get; set; }

        public static int AmountOfPackages { get; set; } 
        
        public static int FailedPackage { get; set; }
    }
}
