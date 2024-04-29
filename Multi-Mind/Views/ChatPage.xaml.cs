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
		AlertDialog("Welcome to Multi-Mind!","Please enjoy your chatting!");
	}

    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        agentLabel.Text = Global.Agent.Model;
        agentLabel.BackgroundColor = Global.Agent.Color;
        agentThemeColor = Global.Agent.Color;
        userThemeColor = Global.Agent.Color;        // test color
    }

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

        string username = "User";

		AddMessageToChat(username, message);

        isSending = false;

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