using System;

namespace Shared.Model
{
    public class BuildDetail
    {
        public int Id { get; set; }

        public Definition Definition { get; set; }

        public DateTimeOffset QueueTime { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset FinishTime { get; set; }

        public string Status { get; set; }
        public string Result { get; set; }
    }
}
