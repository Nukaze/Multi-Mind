using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Mind.Models
{
    public class DeviceDisplayInfo
    {
        public double WidthPx { get; private set; }
        public double HeightPx { get; private set; }
        public double Density { get; private set; }
        public double WidthDp => WidthPx / Density;
        public double HeightDp => HeightPx / Density;
        public string? Orientation { get; private set; }

        public DeviceDisplayInfo()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            this.WidthPx = mainDisplayInfo.Width;
            this.HeightPx = mainDisplayInfo.Height;
            this.Density = mainDisplayInfo.Density;
            this.Orientation = mainDisplayInfo.Orientation.ToString();
        }
    }
}
