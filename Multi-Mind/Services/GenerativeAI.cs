﻿using OpenAI_API;
using OpenAI_API.Completions;


namespace Multi_Mind.Services
{
    class GenerativeAI
    {
        public string apiKey;
        public string provider;


        public GenerativeAI(string apiKey)
        {
            this.apiKey = apiKey;
        }


        public async Task SetProviderConnection(string _apiKey)
        {
            // connet chatgpt or gemini or claude in this mehtod
            provider = _apiKey switch
            {
                string key when key.Contains("sk-proj") => "ChatGPT",
                string key when key.Contains("sk-ant") => "Claude",
                string key when key.Contains("AIza") => "Gemini",
                _ => throw new ArgumentException("Invalid API key")
            };

        }

        public async Task<string> GetAiReply(string message)
        {
            string reply = "";
            // get the API message from the provider connected
            switch (provider)
            {
                case "ChatGPT":
                    reply = await ChatGPT(message);
                    break;

                case "Gemini":
                    //reply = await Gemini(message);
                    break;

                case "Claude":
                    //reply =  await Claude(message);
                    break;

                default:
                    return "provider or api key not found";
                    
            }
            
            return reply;
        }

        public async Task<string> ChatGPT(string message)
        {
            try
            {
                OpenAIAPI openAi = new OpenAIAPI(apiKey);

                CompletionResult result = await openAi.Completions.CreateCompletionAsync(message, max_tokens: 150);

                // Check if the result is not null and if choices are available
                if (result is not null && result.Completions.Count > 0)
                {
                    // Retrieve the completion text from the first completion
                    string reply = result.Completions[0].Text.Trim();
                    return reply;
                }
            } catch (Exception e)
            {
                return $"Error: {e}";
            }
            return "";

        }

        public string Gemini()
        {
            return "";

        }
        

        public string Claude()
        {
            return "";
        }
    }
}
