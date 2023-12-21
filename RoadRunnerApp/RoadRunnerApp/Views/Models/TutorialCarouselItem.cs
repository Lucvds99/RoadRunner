using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp.Views.Models
{
    class CarouselItem
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public string? Image {  get; set; }
        public string? Button { get; set; }
        public required bool hasButton { get; set; }

    }
}
