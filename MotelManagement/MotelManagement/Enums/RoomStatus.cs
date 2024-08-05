using System;
using System.Collections.Generic;

namespace MotelManagement.Enums
{
    public class RoomStatusDefinition
    {
        public int Value { get; set; }
        public string Name { get; set; }

        public static readonly RoomStatusDefinition Unknown = new RoomStatusDefinition(0, "Unknown");
        public static readonly RoomStatusDefinition List = new RoomStatusDefinition(1, "List");
        public static readonly RoomStatusDefinition OnProcess = new RoomStatusDefinition(2, "On Process");
        public static readonly RoomStatusDefinition Repair = new RoomStatusDefinition(3, "Repair");
        public static readonly RoomStatusDefinition Rent = new RoomStatusDefinition(4, "Rent");

        private RoomStatusDefinition(int value, string name) { Value = value; Name = name; }

        private static readonly Dictionary<int, RoomStatusDefinition> Statuses = new Dictionary<int, RoomStatusDefinition>()
        {
            { Unknown.Value, Unknown },
            { List.Value, List },
            { OnProcess.Value, OnProcess },
            { Repair.Value, Repair },
            { Rent.Value, Rent },
        };

        public static RoomStatusDefinition GetStatus(int value)
        {
            if (Statuses.TryGetValue(value, out var status))
            {
                return status;
            }
            return Unknown;
        }
    }
}
