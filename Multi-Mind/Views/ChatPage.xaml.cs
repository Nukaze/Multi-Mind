using Multi_Mind.Services;
using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class ChatPage : ContentPage
{
	bool isSending = false;
    bool isReceiving = false;


    Color agentTextColor = Colors.AliceBlue;
    Color agentThemeColor = Colors.DimGrey;

    Color userTextColor = Colors.Black;
    Color userThemeColor = Colors.AliceBlue;


    public ChatPage()
	{
		InitializeComponent();
	}

    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        agentLabel.Text = Global.Agent.Model;
        agentLabel.BackgroundColor = Global.Agent.Color;
        agentThemeColor = Global.Agent.Color;
        userThemeColor = Global.Agent.Color;        // test color

        sendButton.Padding = new Thickness(100);
        sendButton.Source = "images/send.png";

    }

    [Obsolete]
    private void Send_Button_Clicked(object sender, EventArgs e)
    {
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

        string username = "User";

		AddMessageToChat(username, message);

        Device.BeginInvokeOnMainThread(async () =>
        {
            int randomMsDelay = (int)(new Random().NextDouble() * 2000);
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

        // Create a Frame to contain the label with rounded corners
        var frame = new Frame
        {
            HorizontalOptions = isReceiving ? LayoutOptions.Start : LayoutOptions.End,
            BackgroundColor = isReceiving ? agentThemeColor : userThemeColor,
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