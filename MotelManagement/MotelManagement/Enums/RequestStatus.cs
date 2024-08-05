using System;

namespace MotelManagement.Enums
{
    public class RequestStatusDefinition
    {
        public int Value { get; set; }
        public string Name { get; set; }

        public static readonly RequestStatusDefinition Pending = new RequestStatusDefinition(1, "Pending");
        public static readonly RequestStatusDefinition Approved = new RequestStatusDefinition(2, "Approved");
        public static readonly RequestStatusDefinition Rejected = new RequestStatusDefinition(3, "Rejected");
        public static readonly RequestStatusDefinition Completed = new RequestStatusDefinition(4, "Completed");

        private RequestStatusDefinition(int value, string name)
        {
            Value = value;
            Name = name;
        }

        private static readonly Dictionary<int, RequestStatusDefinition> Statuses = new Dictionary<int, RequestStatusDefinition>()
        {
            { Pending.Value, Pending },
            { Approved.Value, Approved },
            { Rejected.Value, Rejected },
            { Completed.Value, Completed },
        };

        public static RequestStatusDefinition GetStatus(int value)
        {
            return Statuses.TryGetValue(value, out var status) ? status : null;
        }
    }
}
