using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;
using static Multi_Mind.Services.Utilize;


namespace Multi_Mind.Models
{
    public class Agent
    {
        public enum Models
        {
            Null,
            ChatGPT,
            Gemini,
            Claude
        }

        private List<Color> ColorList = new List<Color> {
            Color.FromArgb("#2b2b2b"),  // null
            Color.FromArgb("#75A99C"),  // ChatGPT
            Color.FromArgb("#9975C0"),  // Gemini
            Color.FromArgb("#D09A74")   // Claude
        };

        private List<string> ApiKeyList = new List<string>
        {
            "eiei",  // null
            Environment.GetEnvironmentVariable("CHATGPT_API_KEY") ?? "not found",  // ChatGPT
            Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? "not found",  // Gemini
            Environment.GetEnvironmentVariable("CLAUDE_API_KEY") ?? "not found"   // Claude
        };

        public string Model { get; private set; }
        public short Id { get; private set; }
        public Color Color => ColorList[Id];
        public string ApiKey => ApiKeyList[Id];

        static Agent()
        {
            var env = DotNetEnv.Env.Load();
            Console.WriteLine(env);


        }

        public Agent(Models? model)
        {
            SetModel(model ?? Models.Null);
        }


        public void SetModel(Models model)
        {
            this.Model = model.ToString();
            this.Id = (short)model;
        }

    }
}
