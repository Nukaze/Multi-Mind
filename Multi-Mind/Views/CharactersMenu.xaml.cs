using Multi_Mind.Models;
using static Multi_Mind.Services.Utilize;
using Multi_Mind.Services;


namespace Multi_Mind.Views;

public partial class CharactersMenu : ContentPage
{

    private Characters characters = new Characters();

    public CharactersMenu()
    {
        InitializeComponent();
        CheckScreenDimensions();

        characters = new Characters();
        characters.GetDefaultCharacters();
        GenerateAndBindingCharactersButtons();



    }

    //This method is called when the page is first displayed and have override to update the characters buttons
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GenerateAndBindingCharactersButtons();
    }


    private void GenerateAndBindingCharactersButtons()
    {
        buttonVertStackLayout.Children.Clear();
        foreach (string character in characters.charactersList)
        {
            Button button = new Button
            {
                Text = character,
                HeightRequest = 120,
                CornerRadius = 300,
                BackgroundColor = Global.Agent.Color,
                TextColor = Colors.White,
                FontSize = 32,
                FontAttributes = FontAttributes.Bold,
            };

            buttonVertStackLayout.Children.Add(button);
        }
    }

    private async void CheckScreenDimensions()
    {
        var displayInfo = Global.DeviceDisplayInfo;
        await AlertDialog(
            $"Agent {Global.Agent.Model}",
            $"KEY {Global.Agent.ApiKey}\nDensity: {displayInfo.Density}\nWidthDP: {displayInfo.WidthDp}\nHeightDP: {displayInfo.HeightDp}\nOrientation: {displayInfo.Orientation}",
            "OK"
            );

    }


}