using ElevatorApp;
using System;

List<Elevator> elevators = new List<Elevator>
            {
                new Elevator(1),
                new Elevator(2),
                new Elevator(3)
            };
Random random = new Random();
foreach (var elevator in elevators)
{
    elevator.SetRandomStatus(random);
    elevator.SetRandomPositions(random);
}
Console.WriteLine("Positions of elevators:");
foreach (var elevator in elevators)
{
    Console.WriteLine("Elevator " + elevator.Id +": Status - " + elevator.Status +", Position - " + elevator.Position + " floor");
}
Console.WriteLine();
Console.WriteLine("Push <Enter> for call elevator:");
Console.ReadLine();
Elevator nearestElevator = CallNearestElevator();
Console.WriteLine($"Elevator №{nearestElevator.Id} ({nearestElevator.Status}) is coming");

Elevator CallNearestElevator()
{    
    var availableElevators = elevators.Where(e => e.Status == ElevatorStatus.Free ||
                                                  e.Status == ElevatorStatus.MovingToFirstFloor).ToList();
    if (availableElevators.Count == 0)
        return null;    
    var movingToTheFirstFloorElevators = availableElevators.Where(e => e.Status == ElevatorStatus.MovingToFirstFloor)
                                                         .OrderBy(e => e.Position).ToList();
    if (movingToTheFirstFloorElevators.Count > 1)
        
        return movingToTheFirstFloorElevators[0];
    return availableElevators.OrderBy(e => e.Position).First();
}