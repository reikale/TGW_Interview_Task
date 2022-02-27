using TGW_second_task.Models;
using TGW_second_task;
using TGW_second_task.Controllers;


var app = new AppInitializer();
var conveyorBuilder = new ConveyorController();


while (app.availableDestinations < 0)
{
    app.AskForNumberOfDestinations();
}

while (app.selectedStrategy < 0 || app.selectedStrategy > Enum.GetValues(typeof(Conveyor.destinationSelectionStrategy)).Length-1)
{
    app.AskForDestinationSelectionStrategy();
}


while (app.consecutiveLoads < 0)
{
    app.AskForNumberOfLoadsUpponArrival();
}


while (app.NumberOfPackages <= 0)
{
    app.AskForAmountOfPackages();
}


while (app.failurePercentage < 0 || app.failurePercentage > 100)
{
    app.AskForPercentageOfFailure();
}



LoadController loadBuilder = new LoadController();

app.Results(loadBuilder);
