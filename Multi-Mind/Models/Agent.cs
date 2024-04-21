using Multi_Mind.Environment;
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

        public static readonly string[] ModelsLabel = new string[] { "Null", "ChatGPT", "Gemini", "Claude" };

        private List<Color> ColorList = new List<Color> {
            Color.FromArgb("#2b2b2b"),  // null
            Color.FromArgb("#75A99C"),  // ChatGPT
            Color.FromArgb("#9975C0"),  // Gemini
            Color.FromArgb("#D09A74")   // Claude
        };

        public short Id { get; private set; }
        public string Model { get; private set; }
        public string[] AgentApiKeyList { get; private set; }

        public Color Color => ColorList[Id];
        public string ApiKey => AgentApiKeyList[Id];


        static Agent() { }

        public Agent(Models? model, string[] agentApiKeyList)
        {
            SetModel(model ?? Models.Null);
            SetAgentApiKeyList(agentApiKeyList);
        }


        public void SetModel(Models model)
        {
            this.Model = model.ToString();
            this.Id = (short)model;
        }

        internal void SetAgentApiKeyList(string[] agentApiKeyList)
        {
            this.AgentApiKeyList = agentApiKeyList;
        }

        public string GetModelLabel(short id)
        {
            if (id < 0 || id >= ModelsLabel.Length)
            {
                return ModelsLabel[0];
            }
            return ModelsLabel[id];
        }
    }
}
