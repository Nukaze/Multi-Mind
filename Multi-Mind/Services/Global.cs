using Multi_Mind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Mind.Services
{
    public static class Global
    {
        public static Agent Agent { get; set; } = new Agent(Agent.Models.Null);

        public static DeviceDisplayInfo DeviceDisplayInfo { get; set; } = new DeviceDisplayInfo();

    }
}
