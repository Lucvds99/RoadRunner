using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp.Views.Models
{
    internal class PopupModel
    {
        public required string PopupTitle { get; set; }
        public required string PopupContent { get; set; }
        public required string PopupImageSource { get; set; }
        public string? PopupButtonText1 { get; set; }
        public string? PopupButtonText2 { get; set; }
        public bool? HasButton1 { get; set; }
        public bool? HasButton2 { get; set; }
    }
}
