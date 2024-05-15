using Multi_Mind.Services;
using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class ChatPage : ContentPage
{
	bool isSending = false;
    bool isReceiving = false;
    bool isAgentReply = false;


    Color agentTextColor = Colors.AliceBlue;
    Color agentThemeColor = Colors.DimGrey;

    Color userTextColor = Colors.Black;
    Color userThemeColor = Colors.AliceBlue;


    GenerativeAI genai = new GenerativeAI(Global.Agent.ApiKey);

    public ChatPage()
	{
		InitializeComponent();
	}

    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (Global.Agent.Id == 0)
        {
            await AlertDialogCustom("Agent not found", "Please select an Agent first!");
            await Shell.Current.GoToAsync("//HubAI");
            return;
        }
        if (Global.characterId < 0)
        {
            await AlertDialogCustom("Character not found", "Please select a Character first!");
            await Shell.Current.GoToAsync("//Characters");
            return;
        }

        genai.SetProviderConnection(Global.Agent.ApiKey);


        agentLabel.Text = Global.Agent.Model;
        agentLabel.BackgroundColor = Global.Agent.Color;
        agentThemeColor = Global.Agent.Color;
        userThemeColor = Global.Agent.Color;        // test color

        sendButton.Padding = new Thickness(100);
        sendButton.Source = "images/send.png";

    }

    [Obsolete]
    private async void Send_Button_Clicked(object sender, EventArgs e)
    {
        // prevent multiple messages from user sent
		if (isSending)
		{
            return;
        }
		
		string message = MessageEntry.Text;
		
		if (string.IsNullOrEmpty(message))
		{
			return;
		}

		isSending = true;
        MessageEntry.IsEnabled = false;
        sendButton.IsEnabled = false;
        sendButton.Source = "images/hourglass.png";
        sendButton.Padding = new Thickness(150);

        // user message
        string username = "user";
		AddMessageToChat(username, message);

        // generate responce from ai and switching state
        isReceiving = true;
        string reply = await genai.GetAiReply(message);
        AddMessageToChat(Global.Agent.Model, reply);
        isReceiving = false;


        // clear message entry
        Device.BeginInvokeOnMainThread(async () =>
        {
            int randomMsDelay = (int)(new Random().NextDouble() * 1000);
            await Task.Delay(randomMsDelay);
            MessageEntry.Text = string.Empty;
            isSending = false;
            MessageEntry.IsEnabled = true;
            sendButton.IsEnabled = true;
            sendButton.Source = "images/send.png";
            sendButton.Padding = new Thickness(100);
        });

    }

    private void AddMessageToChat(string sender,string message)
    {
        isAgentReply = sender.ToLower() != "user";

        // Create a Frame to contain the label with rounded corners
        var frame = new Frame
        {
            HorizontalOptions = isAgentReply ? LayoutOptions.Start : LayoutOptions.End,
            BackgroundColor = isAgentReply ? agentThemeColor : userThemeColor,
            CornerRadius = 20,
            Padding = new Thickness(10),
            Margin = new Thickness(5, 8),
        };

        // Create a label to display the message
        var label = new Label
        {
            Text = message,
            TextColor = isReceiving ? agentTextColor : userTextColor,
        };

        // Add the label to the Frame
        frame.Content = label;

        // Add the Frame to the chat interface
        MessagesStackLayout.Children.Add(frame);
    }
}