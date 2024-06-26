﻿using Multi_Mind.Environment;
using Multi_Mind.Models;

using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Services
{
    public static class Global
    {
        public static Env ENV { get; set; } = new Env();

        public static Agent Agent { get; set; } = new Agent(Agent.Models.Null, Global.ENV.AgentApiKeyList);

        public static DeviceDisplayInfo DeviceDisplayInfo { get; set; } = new DeviceDisplayInfo();

        public static short characterId = -1;



        public static User CURRENT_USER = new User();

        public static string UserChatGPTKey = "";

    }
}
