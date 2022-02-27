using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGW_second_task.Models;

namespace TGW_second_task.Controllers
{
    public class ConveyorController
    {
        public void InitializeDestinationsList(int numberOfDestinations)
        {
            var DestinationsSummary = new List<List<string>>();
            for (int n = 0; n < numberOfDestinations; n++)
            {
               var destination = new List<string>();
                DestinationsSummary.Add(destination);
            }
        }

        public void setStrategy(int userSelection)
        {
            Conveyor.SelectedStratedy = (Conveyor.destinationSelectionStrategy)userSelection;
        }

        public void setNumberOfPackages(int userNumberOfPackages)
        {
            Conveyor.AmountOfPackages = userNumberOfPackages;
        }

        public void CalculateFailedLoad(int userFailPercentage)
        {
            decimal failPercent = (decimal)userFailPercentage/100;
            var amountOfLoadsThatWillFail = Conveyor.AmountOfPackages * failPercent;
            var OneEveryXPackageWillFail = Conveyor.AmountOfPackages / amountOfLoadsThatWillFail;
            Conveyor.FailedPackage = (int)Math.Round(OneEveryXPackageWillFail, 0);
        }

    }
}
