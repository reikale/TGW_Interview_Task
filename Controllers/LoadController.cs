using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGW_second_task.Models;

namespace TGW_second_task.Controllers
{
	public class LoadController
	{
		
		public List<Load> LoadsList;

		public LoadController()
		{
			LoadsList = new List<Load>();
			Startup();
		}

		public void Startup()
		{
			DestinationSetter(InitializeLoadsList());
		}

		public List<Load> InitializeLoadsList()
		{
			for (int i = 0; i < Conveyor.AmountOfPackages; i++)
			{
				LoadsList.Add(new Load { Id = i});
			}
			return LoadsList;
		}

		public void DestinationSetter(List<Load> LoadsList)
		{
			var resultList = new List<int>();
			for (int a = 1; a <= Load.NumberOfDestinations; a++)
			{
				resultList.Add(a);
			}
			if (Conveyor.SelectedStratedy == (Conveyor.destinationSelectionStrategy)1)
			{
				var copyOfList = resultList;
				resultList = copyOfList.OrderBy(x => Guid.NewGuid()).ToList();
			}
			Load.ListOfDestinations = resultList;
			Setter(LoadsList);
		}
	
		public void Setter(List<Load> LoadsList)
		{
			
			var positionInList = 0;
			
			var counterForConsecutive = 1;
			var counterForFail = 1;
			foreach (Load item in LoadsList)
			{
				if (counterForConsecutive != Conveyor.ConsecutiveLoads &&
					counterForFail != Conveyor.FailedPackage &&
					positionInList+1 != Load.ListOfDestinations.Count)
				{
					item.Destination = Load.ListOfDestinations[positionInList];
					counterForConsecutive++;
					counterForFail++;
				}
				else if (counterForConsecutive == Conveyor.ConsecutiveLoads &&
					counterForFail != Conveyor.FailedPackage &&
					positionInList + 1 != Load.ListOfDestinations.Count)
				{
					item.Destination = Load.ListOfDestinations[positionInList];
					positionInList++;
					counterForConsecutive = 1;
					counterForFail++;
				}
				else if (counterForConsecutive == Conveyor.ConsecutiveLoads &&
					counterForFail == Conveyor.FailedPackage &&
					positionInList + 1 != Load.ListOfDestinations.Count)
				{
					item.Destination = 0;
					item.FailedLoad = true;
					positionInList++;
					counterForConsecutive = 1;
					counterForFail = 1;
				}
				else if (counterForConsecutive != Conveyor.ConsecutiveLoads &&
					counterForFail == Conveyor.FailedPackage &&
					positionInList + 1 != Load.ListOfDestinations.Count)
				{
					item.Destination = 0;
					item.FailedLoad = true;
					counterForConsecutive++;
					counterForFail = 1;
				}
				else if (counterForConsecutive != Conveyor.ConsecutiveLoads &&
					counterForFail != Conveyor.FailedPackage &&
					positionInList + 1 >= Load.ListOfDestinations.Count)
				{
					item.Destination = Load.ListOfDestinations[positionInList];
					counterForConsecutive++;
					counterForFail++;
				}
				else if (counterForConsecutive == Conveyor.ConsecutiveLoads &&
					counterForFail != Conveyor.FailedPackage &&
					positionInList + 1 >= Load.ListOfDestinations.Count)
				{
					item.Destination = Load.ListOfDestinations[positionInList];
					positionInList = 0;
					counterForConsecutive = 1;
					counterForFail++;
				}
				else if (counterForConsecutive == Conveyor.ConsecutiveLoads &&
					counterForFail == Conveyor.FailedPackage &&
					positionInList + 1 >= Load.ListOfDestinations.Count)
				{
					item.Destination = 0;
					item.FailedLoad = true;
					positionInList = 0;
					counterForConsecutive = 1;
					counterForFail = 1;
				}
				else if (counterForConsecutive != Conveyor.ConsecutiveLoads &&
					counterForFail == Conveyor.FailedPackage &&
					positionInList + 1 == Load.ListOfDestinations.Count)
				{
					item.Destination = 0;
					item.FailedLoad = true;
					counterForConsecutive++;
					counterForFail = 1;
				}
			}
		}
	}
}
