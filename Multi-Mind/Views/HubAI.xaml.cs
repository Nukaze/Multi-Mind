using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class HubAI : ContentPage
{
    public HubAI()
    {
        InitializeComponent();
    }

    private async void ChatGPT_Clicked(object sender, EventArgs e)
    {
        string info = "ChatGPT is a large language model trained on a diverse range of internet text. It can generate human-like responses to given text inputs. ChatGPT is a product of OpenAI.";
        await AlertDialog(
            "Chat with ChatGPT",
            info,
            "Confirm",
            "Cancel"
        );
    }

    private async void Gemini_Clicked(object sender, EventArgs e)
    {
        string info = "Gemini is a powerful language model from Google AI. It can process different types of information and comes in different sizes for various tasks. Gemini is still under development and not widely available yet.";
        await AlertDialog(
            "Chat with Gemini",
            info,
            "Confirm",
            "Cancel"
        );
    }

    private async void Claude_Clicked(object sender, EventArgs e)
    {
        string info = "";
        await AlertDialog(
            "Chat with Claude",
            info,
            "Confirm",
            "Cancel"
        );
    }
}