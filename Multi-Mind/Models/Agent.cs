using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private List<string> AgentApiKey = new List<string>
        {
            "eiei",  // null
            "",  // ChatGPT
            "",  // Gemini
            ""   // Claude
        };

        public string Model { get; private set; }
        public short Id { get; private set; }
        public Color Color => ColorList[Id];


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
