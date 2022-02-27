using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGW_second_task.Controllers;
using TGW_second_task.Models;

namespace TGW_second_task
{
    public class AppInitializer
    {
        ConveyorController conveyorBuilder = new ConveyorController();

        public int availableDestinations = -1;
        public int selectedStrategy = -1;
        bool errorMessageIsVisible = false;
        string errorMessage = "";
        bool success;
        string userInput = string.Empty;
        public int consecutiveLoads = -1;
        public int failurePercentage = -1;
        public int NumberOfPackages = -1;
        

        public int AskForNumberOfDestinations()
        {
            Console.Clear();
            
            if (errorMessageIsVisible)
            {
                Console.WriteLine(errorMessage);
            }
            Console.WriteLine("Provide the number of available destinations (0-n): ");
            userInput = Console.ReadLine();
                success = int.TryParse(userInput, out availableDestinations);
                if (!success)
                {
                    errorMessageIsVisible = true;
                    availableDestinations = -1;
                    errorMessage = "The value user entered is not a numeric value or number is out of range. Please try again.";
                }
                else
                {
                    errorMessageIsVisible = false;
                    conveyorBuilder.InitializeDestinationsList(availableDestinations);
                    Load.NumberOfDestinations = availableDestinations;
                }
                        
            return availableDestinations;
        }


        public void AskForDestinationSelectionStrategy()
        {
            Console.Clear();
            if (errorMessageIsVisible)
            {
                Console.WriteLine(errorMessage);
            }
            Console.WriteLine("Please select a destination selection strategy from the list:");
            int count = 0;
            foreach (var item in Enum.GetValues(typeof(Conveyor.destinationSelectionStrategy)))
            {
                Console.WriteLine($"{count} - {item}");
                count++;
            }
            userInput = Console.ReadLine();
            success = int.TryParse(userInput, out selectedStrategy);
            if (!success || selectedStrategy < 0)
            {
                errorMessageIsVisible = true;
                selectedStrategy = -1;
                errorMessage = "The value user entered is not a numeric value or number is out of range. Please try again.";
            }
            else
            {
                errorMessageIsVisible = false;
                conveyorBuilder.setStrategy(selectedStrategy);
            }
        }


        public void AskForNumberOfLoadsUpponArrival()
        {
            Console.Clear();
            if (errorMessageIsVisible)
            {
                Console.WriteLine(errorMessage);
            }
            Console.WriteLine("Please select a number of consecutive loads:");
            userInput = Console.ReadLine();
            success = int.TryParse(userInput, out consecutiveLoads);
            if (!success)
            {
                errorMessageIsVisible = true;
                consecutiveLoads = -1;
                errorMessage = "The value user entered is not a numeric value or number is out of range. Please try again.";
            }
            else
            {
                errorMessageIsVisible = false;
                Conveyor.ConsecutiveLoads = consecutiveLoads;
            }
        }
        

        public void AskForAmountOfPackages()
        {
            Console.Clear();
            if (errorMessageIsVisible)
            {
                Console.WriteLine(errorMessage);

            }
            Console.WriteLine("Please provide the number of packages that will be in circulating in the system:");
            userInput = Console.ReadLine();
            success = int.TryParse(userInput, out NumberOfPackages);
            if (!success)
            {
                errorMessageIsVisible = true;
                NumberOfPackages = -1;
                errorMessage = "The value user entered is not a numeric value or number is out of range. Please try again.";
            }
            else
            {
                errorMessageIsVisible = false;
                conveyorBuilder.setNumberOfPackages(NumberOfPackages);
            }
        }


        public void AskForPercentageOfFailure()
        {
            Console.Clear();
            if (errorMessageIsVisible)
            {
                Console.WriteLine(errorMessage);
            }
            Console.WriteLine("Please select a percentage of failure for load to be diverted into its destination:");
            userInput = Console.ReadLine();
            success = int.TryParse(userInput, out failurePercentage);
            if (!success)
            {
                errorMessageIsVisible = true;
                failurePercentage = -1;
                errorMessage = "The value user entered is not a numeric value or number is out of range. Please try again.";
            }
            else
            {
                errorMessageIsVisible = false;
                conveyorBuilder.CalculateFailedLoad(failurePercentage);
            }
        }


        public void Results(LoadController loadBuilder)
        {
            Console.Clear();
            var ReachedDestinations = CalculateReachedDestinations(loadBuilder);
            Console.WriteLine("These destinations were reached: \n");
            foreach (var destination in ReachedDestinations)
            {
                var itemsInDestination = loadBuilder.LoadsList.Where(x => x.Destination == destination).ToList().Count;
                var percentOfAllLoads = (itemsInDestination * 100) / Conveyor.AmountOfPackages;
                Console.Write($"Destination: { destination}, percentage of all loads: {percentOfAllLoads}%.\n");
                
            }
        }


        public List<int> CalculateReachedDestinations(LoadController loadBuilder)
        {
            var ReachedDestinations = new List<int>();
            foreach (var item in loadBuilder.LoadsList)
            {
                if (!ReachedDestinations.Contains(item.Destination))
                {
                    ReachedDestinations.Add(item.Destination);
                }
            }
            return ReachedDestinations;
        }
    }
}
