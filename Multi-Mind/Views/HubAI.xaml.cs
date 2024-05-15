using Multi_Mind.Models;
using Multi_Mind.Services;
using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class HubAI : ContentPage
{

    public HubAI()
    {
        InitializeComponent();
        HubAI.WelcomeLanding();
    }

    private static async void WelcomeLanding()
    {
        await AlertDialogCustom("Notify", $"Welcome to Multi Mind | {App.CurrentUser.Username} ");
    }

    private async void ChatGPT_Clicked(object sender, EventArgs e)
    {
        string info = "ChatGPT is a large language model trained on a diverse range of internet text. It can generate human-like responses to given text inputs. ChatGPT is a product of OpenAI.";
        await AlertDialogCustom(
            "Chat with ChatGPT",
            info,
            "Confirm",
            "Cancel",
            onAccept: () =>
            {
                Global.Agent.SetModel(Agent.Models.ChatGPT);
                GoToCharactersMenu();
            }
        );
    }

    private async void Gemini_Clicked(object sender, EventArgs e)
    {
        string info = "Gemini is a powerful language model from Google AI. It can process different types of information and comes in different sizes for various tasks. Gemini is still under development and not widely available yet.";
        await AlertDialogCustom(
            "Chat with Gemini",
            info,
            "Confirm",
            "Cancel",
            onAccept: () =>
            {
                Global.Agent.SetModel(Agent.Models.Gemini);
                GoToCharactersMenu();
            }
        );
    }

    private async void Claude_Clicked(object sender, EventArgs e)
    {
        string info = "Claude is an AI chatbot developed by Anthropic. It focuses on being helpful, harmless, and honest in its responses.";
        await AlertDialogCustom(
            "Chat with Claude",
            info,
            "Confirm",
            "Cancel",
            onAccept: () =>
            {
                Global.Agent.SetModel(Agent.Models.Claude);
                GoToCharactersMenu();
            }
        );
    }

    private async void HowToGetAPIKey_Clicked(object sender, EventArgs e)
    {
        string _rawHowToGetAPIKeyUrl = "https://www.youtube.com/watch?v=OB99E7Y1cMA";
        
        
        await LoadingDialog(true, OpenHowToGetAPIKeyViaYoutube);
    }

    private async Task<dynamic> OpenHowToGetAPIKeyViaYoutube()
    {
        string videoId = "OB99E7Y1cMA";
        Uri _uri = new Uri($"vnd.youtube://watch/{videoId}");
        return await Browser.OpenAsync(_uri);
    }

    private async void GoToCharactersMenu()
    {
        await Shell.Current.GoToAsync("//Characters");
    }

    private async void userApiKeySubmit_Clicked(object sender, EventArgs e)
    {
        string userApiKey = userApiKeyEntry.Text;

        if (string.IsNullOrEmpty(userApiKey))
        { 
            await AlertDialogCustom("Notify", "Please enter the user API key");
            return;
        }

        bool apiKeyExists = !string.IsNullOrEmpty(Global.UserChatGPTKey);
        string message = apiKeyExists ? "Do you want to replace the user API key?" : "Do you want to add the user API key?";

        bool isAccpeted = await AlertDialogCustom("Notify",
                                            message,
                                            "Confirm",
                                            "Cancel",
                                            onAccept: async () =>
                                            {
                                                Global.UserChatGPTKey = userApiKey;
                                                string successMessage = apiKeyExists ? "User API key has been replaced" : "User API key has been set";
                                                await AlertDialogCustom("Notify", successMessage);
                                                await AlertDialogCustom("Notify", $"User API key: {Global.UserChatGPTKey}");
                                            }
        );
    }

    private void clearApiKeyButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(userApiKeyEntry.Text))
        {
            return;
        }

        userApiKeyEntry.Text = "";

    }
}