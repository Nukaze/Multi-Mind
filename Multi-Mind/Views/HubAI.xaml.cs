using Multi_Mind.Models;
using Multi_Mind.Services;
using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class HubAI : ContentPage
{

    public HubAI()
    {
        InitializeComponent();
        AlertDialog("Welcome", $"Agent Name {Global.Agent.Model}");
    }

    private async void ChatGPT_Clicked(object sender, EventArgs e)
    {
        string info = "ChatGPT is a large language model trained on a diverse range of internet text. It can generate human-like responses to given text inputs. ChatGPT is a product of OpenAI.";
        await AlertDialog(
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
        await AlertDialog(
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
        await AlertDialog(
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

    private async void GoToCharactersMenu()
    {
        await Navigation.PushAsync(new CharactersMenu());
    }
}