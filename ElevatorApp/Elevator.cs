using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorApp
{
    public enum ElevatorStatus
    {
        Free, Busy, MovingToFirstFloor
    }
    public class Elevator
    {
        public int Id {  get; set; }
        public ElevatorStatus Status { get; set; }
        public int Position {  get; set; }

        public Elevator(int numberOfElevator)
        {
            Id = numberOfElevator;
        }

        public void SetRandomStatus(Random random)
        {
            Array statuses = Enum.GetValues(typeof(ElevatorStatus));
            Status = (ElevatorStatus)statuses.GetValue(random.Next(statuses.Length));
        }

        public void SetRandomPositions(Random random)
        {
            Position = random.Next(1, 26);
        }
    }
    
}
