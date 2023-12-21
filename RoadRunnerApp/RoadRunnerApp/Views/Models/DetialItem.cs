using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp.Views.Models
{
    class DetialItem
    {
        public required string headerIcon { get; set; }
        public required string Title { get; set; }
        public required string HeaderInfo { get; set; }
        public string? image { get; set; }
        public required string Description { get; set; }
        public string? Button { get; set; }
        public required bool hasButton { get; set; }
    }
}
