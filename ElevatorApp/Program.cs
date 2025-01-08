using ElevatorApp;
using System;
using System.Threading.Channels;
List<Elevator> elevators = new List<Elevator>
            {
                new Elevator(1),
                new Elevator(2),
                new Elevator(3)
            };
while (true)
{
    Random random = new Random();
    foreach (var elevator in elevators)
    {
        elevator.SetRandomStatus(random);
        elevator.SetRandomPositions(random);
    }
    Console.WriteLine("Positions of elevators:");
    foreach (var elevator in elevators)
    {
        Console.WriteLine("Elevator " + elevator.Id + ": Status - " + elevator.Status + ", Position - " + elevator.Position + " floor");
    }
    Console.WriteLine();
    Console.WriteLine("Push <Enter> for call elevator or <exit> for quit:");
    string input = Console.ReadLine();
    if (input.ToUpper() == "EXIT")
    {
        Console.WriteLine("Good bye");
        break;
    }
    if (string.IsNullOrEmpty(input))
    {
        Elevator nearestElevator = CallNearestElevator();
        Console.WriteLine($"Elevator №{nearestElevator.Id} ({nearestElevator.Status}) is coming");
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("Input incorrect!");
        Console.WriteLine();
    }
}

Elevator CallNearestElevator()
{
    var availableElevators = elevators.Where(e => e.Status == ElevatorStatus.Free ||
                                                  e.Status == ElevatorStatus.MovingToFirstFloor).ToList();
    var busyElevators = elevators.Where(e => e.Status == ElevatorStatus.Busy).OrderBy(e => e.Position).ToList();
    if (availableElevators.Count == 0)
        return busyElevators.First();
    var movingToTheFirstFloorElevators = availableElevators.Where(e => e.Status == ElevatorStatus.MovingToFirstFloor)
                                                         .OrderBy(e => e.Position).ToList();
    if (movingToTheFirstFloorElevators.Count > 1)

        return movingToTheFirstFloorElevators[0];
    return availableElevators.OrderBy(e => e.Position).First();
}